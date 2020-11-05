using System;

using static Ax.Engine.Core.Native;

namespace Ax.Engine.Core.Rendering
{
    public sealed class SingleBufferedOutputHandler : OutputHandler
    {
        public OutputHandlerBufferInfo Buffer { get; set; }

        public SingleBufferedOutputHandler(OutputHandlerInfo info = default)
            : base(info)
        { }

        public override void Enable()
        {
            GetStdOut(out IntPtr bufferPtr);

            CONSOLE_FONT_INFOEX bufferFont = Info.font;
            CONSOLE_MODE_OUTPUT lastMode = 0;

            SetupBuffer(bufferPtr, ref bufferFont, ref lastMode);

            Buffer = new OutputHandlerBufferInfo(bufferPtr, (uint)lastMode);
        }

        public override void Disable()
        {
            CONSOLE_FONT_INFOEX lastFont = Info.font;
            CONSOLE_MODE_OUTPUT lastMode = (CONSOLE_MODE_OUTPUT)Buffer.lastMode;

            SetupBuffer(Buffer.ptr, ref lastFont, ref lastMode);

            SetConsoleMode(Buffer.ptr, Buffer.lastMode);
        }

        public override void Write(byte[] buffer, int count)
        {
            WriteConsole(Buffer.ptr, buffer, count, out _, new IntPtr(0));
        }

        public override void EndWrite()
        { }
    }
}
