using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        public const int FIXED_WIDTH_TRUETYPE = 54;

        /// <summary>
        ///  Retrieves the size of the font used by the specified console screen buffer.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the GENERIC_READ access right</param>
        /// <param name="nFont">The index of the font whose size is to be retrieved.</param>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern COORD GetConsoleFontSize(IntPtr hConsoleOutput, int nFont);

        /// <summary>
        ///  Sets extended information about the current console font.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_WRITE"/> access right.</param>
        /// <param name="bMaximumWindow">If this parameter is TRUE, font information is set for the maximum window size. If this parameter is FALSE, font information is set for the current window size.</param>
        /// <param name="lpConsoleCurrentFontEx">A pointer to a <see cref="CONSOLE_FONT_INFOEX"/> structure that contains the font information.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetCurrentConsoleFontEx([In] IntPtr hConsoleOutput, [In] bool bMaximumWindow, [In] ref CONSOLE_FONT_INFOEX lpConsoleCurrentFontEx);

        /// <summary>
        ///  Retrieves information about the current console font.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the GENERIC_READ access right.</param>
        /// <param name="bMaximumWindow">If this parameter is TRUE, font information is retrieved for the maximum window size. If this parameter is FALSE, font information is retrieved for the current window size.</param>
        /// <param name="lpConsoleCurrentFont">A <see cref="CONSOLE_FONT_INFO"/> structure that contains the requested font information.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)] public static extern bool GetCurrentConsoleFont(IntPtr hConsoleOutput, bool bMaximumWindow, [Out] CONSOLE_FONT_INFOEX lpConsoleCurrentFont);

        /// <summary>
        ///  Retrieves extended information about the current console font.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the GENERIC_READ access right.</param>
        /// <param name="bMaximumWindow">If this parameter is TRUE, font information is retrieved for the maximum window size. If this parameter is FALSE, font information is retrieved for the current window size.</param>
        /// <param name="lpConsoleCurrentFontEx">A <see cref="CONSOLE_FONT_INFOEX"/> structure that contains the requested font information.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)] public static extern bool GetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool bMaximumWindow, ref CONSOLE_FONT_INFOEX lpConsoleCurrentFontEx);
    }
}
