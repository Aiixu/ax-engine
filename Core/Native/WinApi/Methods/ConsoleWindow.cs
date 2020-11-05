using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core.Native
{
    public static partial class WinApi
    {
        /// <summary>
        ///  Sets the current size and position of a console screen buffer's window.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_READ"/> access right.</param>
        /// <param name="bAbsolute">If this parameter is TRUE, the coordinates specify the new upper-left and lower-right corners of the window. If it is FALSE, the coordinates are relative to the current window-corner coordinates.</param>
        /// <param name="lpConsoleWindow">A pointer to a <see cref="SMALL_RECT"/> structure that specifies the new upper-left and lower-right corners of the window.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleWindowInfo([In] IntPtr hConsoleOutput, [In] bool bAbsolute, [In] ref SMALL_RECT lpConsoleWindow);
    }
}
