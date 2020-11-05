using System.Runtime.InteropServices;

using static Ax.Engine.Core.Native.WinGdi;

namespace Ax.Engine.Core.Native
{
    public static partial class WinApi
    {
        /// <summary>
        ///  Contains extended information about a console screen buffer.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct CONSOLE_SCREEN_BUFFER_INFOEX
        {
            /// <summary>
            ///  The size of this structure, in bytes.
            /// </summary>
            public uint cbSize;

            /// <summary>
            ///  A <see cref="COORD"/> structure that contains the size of the console screen buffer, in character columns and rows.
            /// </summary>
            public COORD dwSize;

            /// <summary>
            ///  A <see cref="COORD"/> structure that contains the column and row coordinates of the cursor in the console screen buffer.
            /// </summary>
            public COORD dwCursorPosition;

            /// <summary>
            ///  A <see cref="COORD"/> structure that contains the maximum size of the console window, in character columns and rows, given the current screen buffer size and font and the screen size.
            /// </summary>
            public COORD dwMaximumWindowSize;

            /// <summary>
            ///  The attributes of the characters written to a screen buffer by the WriteFile and <see cref="WriteConsole(System.IntPtr, byte[], int, out int, System.IntPtr)"/></see> functions, or echoed to a screen buffer by the ReadFile and ReadConsole functions. For more information, see <see cref="CHAR_ATTRIBUTE"/>.
            /// </summary>
            public short wAttributes;

            /// <summary>
            ///  A <see cref="SMALL_RECT"/> structure that contains the console screen buffer coordinates of the upper-left and lower-right corners of the display window.
            /// </summary>
            public SMALL_RECT srWindow;

            /// <summary>
            ///  The fill attribute for console pop-ups.
            /// </summary>
            public ushort wPopupAttributes;

            /// <summary>
            ///  If this member is TRUE, full-screen mode is supported; otherwise, it is not.
            /// </summary>
            public bool bFullscreenSupported;

            /// <summary>
            ///  An array of <see cref="COLORREF"/> values that describe the console's color settings.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] public COLORREF[] ColorTable;

            /// <summary>
            ///  Create an instance of a <see cref="CONSOLE_SCREEN_BUFFER_INFOEX"/> with fixed <see cref="cbSize"/> (96).
            /// </summary>
            public static CONSOLE_SCREEN_BUFFER_INFOEX Create()
            {
                return new CONSOLE_SCREEN_BUFFER_INFOEX { cbSize = 96 };
            }
        }
    }
}