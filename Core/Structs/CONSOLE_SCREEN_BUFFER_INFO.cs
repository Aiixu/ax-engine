namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Contains information about a console screen buffer.
        /// </summary>
        public struct CONSOLE_SCREEN_BUFFER_INFO
        {
            /// <summary>
            ///  A <see cref="COORD"/> structure that contains the size of the console screen buffer, in character columns and rows.
            /// </summary>
            public COORD dwSize;

            /// <summary>
            ///  A <see cref="COORD"/> structure that contains the column and row coordinates of the cursor in the console screen buffer.
            /// </summary>
            public COORD dwCursorPosition;

            /// TODO: see cref writefile

            /// <summary>
            ///  The attributes of the characters written to a screen buffer by the WriteFile and <see cref="WriteConsole(System.IntPtr, byte[], int, out int, System.IntPtr)"/></see> functions, or echoed to a screen buffer by the ReadFile and ReadConsole functions. For more information, see <see cref="CHAR_ATTRIBUTE"/>.
            /// </summary>
            public short wAttributes;

            /// <summary>
            ///  A <see cref="SMALL_RECT"/> structure that contains the console screen buffer coordinates of the upper-left and lower-right corners of the display window.
            /// </summary>
            public SMALL_RECT srWindow;

            /// <summary>
            ///  A <see cref="COORD"/> structure that contains the maximum size of the console window, in character columns and rows, given the current screen buffer size and font and the screen size.
            /// </summary>
            public COORD dwMaximumWindowSize;
        }
    }
}
