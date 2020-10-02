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
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetConsoleCursorInfo(IntPtr hConsoleOutput, [Out] out CONSOLE_CURSOR_INFO lpConsoleCursorInfo);


        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleCursorInfo(IntPtr hConsoleOutput, [In] ref CONSOLE_CURSOR_INFO lpConsoleCursorInfo);


        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleCursorPosition(IntPtr hConsoleOutput, COORD dwCursorPosition);
    }
}