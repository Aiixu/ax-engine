using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Retrieves information about the size and visibility of the cursor for the specified console screen buffer.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the GENERIC_READ access right.</param>
        /// <param name="lpConsoleCursorInfo">A <see cref="CONSOLE_CURSOR_INFO"/> structure that contains information about the console's cursor.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetConsoleCursorInfo(IntPtr hConsoleOutput, [Out] out CONSOLE_CURSOR_INFO lpConsoleCursorInfo);

        /// <summary>
        ///  Sets the size and visibility of the cursor for the specified console screen buffer.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_READ"/> access right.</param>
        /// <param name="lpConsoleCursorInfo">A pointer to a <see cref="CONSOLE_CURSOR_INFO"/> structure that provides the new specifications for the console screen buffer's cursor.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleCursorInfo(IntPtr hConsoleOutput, [In] ref CONSOLE_CURSOR_INFO lpConsoleCursorInfo);

        /// <summary>
        ///  Sets the cursor position in the specified console screen buffer.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_READ"/> access right.</param>
        /// <param name="dwCursorPosition">A <see cref="COORD"/> structure that specifies the new cursor position, in characters. The coordinates are the column and row of a screen buffer character cell. The coordinates must be within the boundaries of the console screen buffer.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleCursorPosition(IntPtr hConsoleOutput, COORD dwCursorPosition);
    }
}