using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Retrieves the display mode of the current console.
        /// </summary>
        /// <param name="lpModeFlags">The display mode of the console.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetConsoleDisplayMode([Out] out uint lpModeFlags);

        /// <summary>
        ///  Sets the display mode of the specified console screen buffer.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer.</param>
        /// <param name="dwFlags">The display mode of the console. See <see cref="CONSOLE_WINDOW_MODE"/></param>
        /// <param name="lpNewScreenBufferDimensions">A pointer to a <see cref="COORD"/> structure that receives the new dimensions of the screen buffer, in characters.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleDisplayMode(IntPtr hConsoleOutput, uint dwFlags, [Out, Optional] out COORD lpNewScreenBufferDimensions);

        /// <summary>
        ///  Retrieves the original title for the current console window.
        /// </summary>
        /// <param name="lpConsoleTitle">A pointer to a buffer that receives a null-terminated string containing the original title.</param>
        /// <param name="nSize">The size of the <paramref name="lpConsoleTitle"/> buffer, in characters.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleOriginalTitle([Out] out StringBuilder lpConsoleTitle, uint nSize);

        /// <summary>
        ///  Retrieves the title for the current console window.
        /// </summary>
        /// <param name="lpConsoleTitle">A pointer to a buffer that receives a null-terminated string containing the title. If the buffer is too small to store the title, the function stores as many characters of the title as will fit in the buffer, ending with a null terminator.</param>
        /// <param name="nSize">The size of the buffer pointed to by the <paramref name="lpConsoleTitle"/> parameter, in characters.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleTitle([Out] StringBuilder lpConsoleTitle, uint nSize);

        /// <summary>
        ///  Sets the title for the current console window.
        /// </summary>
        /// <param name="lpConsoleTitle">The string to be displayed in the title bar of the console window. The total size must be less than 64K.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll")] public static extern bool SetConsoleTitle([In] string lpConsoleTitle);
    }
}
