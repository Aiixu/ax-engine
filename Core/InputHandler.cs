using System;

using static Ax.Engine.Core.Native;

namespace Ax.Engine.Core
{
    public sealed class InputHandler
    {
        public IntPtr HIN { get => hIn; }

        private IntPtr hIn;
        private CONSOLE_MODE_INPUT inLast;

        public bool Enable()
        {
            if (!GetStdIn(out hIn)) { return false; }
            if (!GetConsoleModeOut(HIN, out inLast)) { return false; }

            CONSOLE_MODE_INPUT mode = inLast | CONSOLE_MODE_INPUT.ENABLE_VIRTUALTERMINALINPUT;

            return SetConsoleMode(HIN, (uint)mode);
        }

        public bool Disable()
        {
            this.hIn = IntPtr.Zero;
            return GetStdIn(out IntPtr hIn) && SetConsoleMode(hIn, (uint)inLast);
        }

        private bool GetStdIn(out IntPtr handle)
        {
            handle = GetStdHandle(STD_INPUT_HANDLE);
            return handle != INVALID_HANDLE;
        }

        private bool GetConsoleModeOut(IntPtr hConsoleHandle, out CONSOLE_MODE_INPUT mode)
        {
            if (!GetConsoleMode(hConsoleHandle, out uint lpMode))
            {
                mode = 0;
                return false;
            }

            mode = (CONSOLE_MODE_INPUT)lpMode;
            return true;
        }
    }
}
