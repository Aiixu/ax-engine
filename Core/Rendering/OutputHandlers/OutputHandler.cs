using System;
using System.Runtime.InteropServices;

using static Ax.Engine.Core.Native.WinApi;

namespace Ax.Engine.Core.Rendering
{
    public abstract class OutputHandler
    {
        public IntPtr Handle { get; protected set; }

        public OutputHandlerInfo Info { get; private set; }

        public OutputHandler(OutputHandlerInfo info = default)
        {
            Info = info;
        }

        public abstract void Enable();

        public abstract void Disable();

        public abstract void Write(byte[] buffer, int count);

        public virtual void EndWrite() { }

        protected bool GetStdOut(out IntPtr handle)
        {
            handle = GetStdHandle((uint)HANDLE.STD_OUTPUT_HANDLE);
            return handle != INVALID_HANDLE;
        }

        protected bool GetConsoleModeOut(IntPtr hConsoleHandle, out CONSOLE_MODE_OUTPUT mode)
        {
            if (!GetConsoleMode(hConsoleHandle, out uint lpMode))
            {
                mode = 0;
                return false;
            }

            mode = (CONSOLE_MODE_OUTPUT)lpMode;
            return true;
        }

        protected void SetupBuffer(IntPtr buffer, ref CONSOLE_FONT_INFOEX fontInfo, ref CONSOLE_MODE_OUTPUT outLast)
        {
            // Console mode
            GetConsoleModeOut(buffer, out outLast);

            CONSOLE_MODE_OUTPUT mode = outLast | CONSOLE_MODE_OUTPUT.ENABLE_VIRTUAL_TERMINAL_PROCESSING;

            SetConsoleMode(buffer, (uint)mode);

            // Font
            CONSOLE_FONT_INFOEX lastFont = new CONSOLE_FONT_INFOEX();
            GetCurrentConsoleFontEx(buffer, false, ref lastFont);

            if(fontInfo.Equals(default(CONSOLE_FONT_INFOEX)))
            {
                fontInfo = lastFont;
                Info.SetFont(lastFont);
            }

            CONSOLE_FONT_INFOEX newFont = new CONSOLE_FONT_INFOEX();

            newFont.cbSize = (uint)Marshal.SizeOf(newFont);
            newFont.FaceName = fontInfo.FaceName;
            newFont.dwFontSize.X = fontInfo.dwFontSize.X;
            newFont.dwFontSize.Y = fontInfo.dwFontSize.Y;

            SetCurrentConsoleFontEx(buffer, false, ref newFont);

            SetConsoleScreenBufferSize(buffer, Info.size);
        }
    }
}
