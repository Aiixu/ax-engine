using System;

using static Ax.Engine.Core.Native;

namespace Ax.Engine.Core.Rendering
{
    public sealed class DoubleBufferedOutputHandler : OutputHandler
    {
        public const int BUFFER_COUNT = 2;

        public int WritingBuffer { get; private set; }
        public OutputHandlerBufferInfo[] Buffers { get; private set; }

        public DoubleBufferedOutputHandler(OutputHandlerInfo info = default)
            : base(info)
        { }

        public override void Enable()
        {
            Buffers = new OutputHandlerBufferInfo[BUFFER_COUNT];
            for (int i = 0; i < BUFFER_COUNT; i++)
            {
                IntPtr bufferPtr = CreateConsoleScreenBuffer(
                    (long)(BUFFER_ACCESS_MODE.GENERIC_WRITE | BUFFER_ACCESS_MODE.GENERIC_READ),
                    (uint)(BUFFER_SHARE_MODE.FILE_SHARE_WRITE | BUFFER_SHARE_MODE.FILE_SHARE_READ),
                    new IntPtr(0), 1, new IntPtr(0));

                CONSOLE_FONT_INFOEX bufferFont = Info.font;
                CONSOLE_MODE_OUTPUT lastMode = 0;

                SetupBuffer(bufferPtr, ref bufferFont, ref lastMode);

                Buffers[i] = new OutputHandlerBufferInfo(bufferPtr, (uint)lastMode);
            }

            WritingBuffer = 1;
            SetConsoleActiveScreenBuffer(Buffers[0].ptr);
        }

        public override void Disable()
        {
            OutputHandlerBufferInfo bufferInfo = Buffers[0];

            CONSOLE_FONT_INFOEX lastFont = Info.font;
            CONSOLE_MODE_OUTPUT lastMode = 0;

            SetupBuffer(bufferInfo.ptr, ref lastFont, ref lastMode);
            SetConsoleActiveScreenBuffer(bufferInfo.ptr);

            SetConsoleMode(bufferInfo.ptr, bufferInfo.lastMode);

            Buffers = null;
            WritingBuffer = -1;
        }

        public override void Write(byte[] buffer, int count)
        {
            WriteConsole(Buffers[WritingBuffer].ptr, buffer, count, out _, new IntPtr(0));
        }

        public override void EndWrite()
        {
            // swap buffers
            SetConsoleActiveScreenBuffer(Buffers[WritingBuffer].ptr);
            WritingBuffer = (WritingBuffer + 1) % BUFFER_COUNT;

            // clear buffer
            byte[] clearSequence = new byte[] { 27, 91, 50, 74 };
            Write(clearSequence, 4);
        }
    }
}
