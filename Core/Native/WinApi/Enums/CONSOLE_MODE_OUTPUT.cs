namespace Ax.Engine.Core.Native
{
    public static partial class WinApi
    {
        /// <summary>
        /// Define flags to change the output mode of the console;
        /// </summary>
        public enum CONSOLE_MODE_OUTPUT : uint
        {
            /// TODO : WriteFile, ReadFile, WriteConsole

            /// <summary>
            ///  Characters written by the <see cref="WriteFile"/> or <see cref="WriteConsole(System.IntPtr, byte[], int, out int, System.IntPtr)"/> function or echoed by the <see cref="ReadFile"/> or <see cref="ReadConsole(System.IntPtr, System.Text.StringBuilder, uint, out uint, System.IntPtr)"/> function are parsed for ASCII control sequences, and the correct action is performed. Backspace, tab, bell, carriage return, and line feed characters are processed.
            /// </summary>
            ENABLE_PROCESSED_OUTPUT = 0x0001,

            /// <summary>
            ///  When writing with <see cref="WriteFile"/> or <see cref="WriteConsole(System.IntPtr, byte[], int, out int, System.IntPtr)"/> or echoing with <see cref="ReadFile"/> or <see cref="ReadConsole(System.IntPtr, System.Text.StringBuilder, uint, out uint, System.IntPtr)"/>, the cursor moves to the beginning of the next row when it reaches the end of the current row. This causes the rows displayed in the console window to scroll up automatically when the cursor advances beyond the last row in the window. It also causes the contents of the console screen buffer to scroll up (discarding the top row of the console screen buffer) when the cursor advances beyond the last row in the console screen buffer. If this mode is disabled, the last character in the row is overwritten with any subsequent characters.
            /// </summary>
            ENABLE_WRAP_AT_EOL_OUTPUT = 0x0002,

            /// <summary>
            ///  When writing with <see cref="WriteFile"/> or  <see cref="WriteConsole(System.IntPtr, byte[], int, out int, System.IntPtr)"/>, characters are parsed for VT100 and similar control character sequences that control cursor movement, color/font mode, and other operations that can also be performed via the existing Console APIs. For more information, see Console Virtual Terminal Sequences.
            /// </summary>
            ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004,

            /// <summary>
            ///  When writing with <see cref="WriteFile"/> or <see cref="WriteConsole(System.IntPtr, byte[], int, out int, System.IntPtr)"/>, this adds an additional state to end-of-line wrapping that can delay the cursor move and buffer scroll operations. 
            ///  Normally when <see cref="ENABLE_WRAP_AT_EOL_OUTPUT"/> is set and text reaches the end of the line, the cursor will immediately move to the next line and the contents of the buffer will scroll up by one line. In contrast with this flag set, the scroll operation and cursor move is delayed until the next character arrives. The written character will be printed in the final position on the line and the cursor will remain above this character as if <see cref="ENABLE_WRAP_AT_EOL_OUTPUT"/> was off, but the next printable character will be printed as if <see cref="ENABLE_WRAP_AT_EOL_OUTPUT"/> is on. No overwrite will occur. 
            ///  Specifically, the cursor quickly advances down to the following line, a scroll is performed if necessary, the character is printed, and the cursor advances one more position. The typical usage of this flag is intended in conjunction with setting <see cref="ENABLE_VIRTUAL_TERMINAL_PROCESSING"/> to better emulate a terminal emulator where writing the final character on the screen (in the bottom right corner) without triggering an immediate scroll is the desired behavior.
            /// </summary>
            DISABLE_NEWLINE_AUTO_RETURN = 0x0008,

            /// <summary>
            ///  Setting this console mode flag will allow these attributes to be used in every code page on every language.
            ///  It is off by default to maintain compatibility with known applications that have historically taken advantage of the console ignoring these flags on non-CJK machines to store bits in these fields for their own purposes or by accident.
            /// </summary>
            ENABLE_LVB_GRID_WORLDWIDE = 0x0010
        }
    }
}