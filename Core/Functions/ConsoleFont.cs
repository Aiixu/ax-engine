using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Retrieves the size of the font used by the specified console screen buffer.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the GENERIC_READ access right</param>
        /// <param name="nFont">The index of the font whose size is to be retrieved.</param>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern COORD GetConsoleFontSize(IntPtr hConsoleOutput, int nFont);
    }
}
