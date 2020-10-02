using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Sets the attributes of characters written to the console screen buffer by the WriteFile or WriteConsole function, or echoed by the ReadFile or ReadConsole function. This function affects text written after the function call.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_READ"/> access right.</param>
        /// <param name="wAttributes">The character attributes. See <see cref="CHAR_ATTRIBUTE"/>.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleTextAttribute([In] IntPtr hConsoleOutput, [In] ushort wAttributes);
    }
}
