using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Flushes the console input buffer. All input records currently in the input buffer are discarded.
        /// </summary>
        /// <param name="hConsoleInput">A handle to the console input buffer. The handle must have the GENERIC_WRITE access right.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool FlushConsoleInputBuffer([In] IntPtr hConsoleInput);

        /// <summary>
        ///  Retrieves the number of unread input records in the console's input buffer.
        /// </summary>
        /// <param name="hConsoleInput">A handle to the console input buffer. The handle must have the GENERIC_READ access right.</param>
        /// <param name="lpcNumberOfEvents">A pointer to a variable that receives the number of unread input records in the console's input buffer.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetNumberOfConsoleInputEvents([In] IntPtr hConsoleInput, [Out] out uint lpcNumberOfEvents);

        /// <summary>
        ///  Retrieves the number of buttons on the mouse used by the current console.
        /// </summary>
        /// <param name="lpNumberOfMouseButtons">A pointer to a variable that receives the number of mouse buttons.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetNumberOfConsoleMouseButtons([Out] out uint lpNumberOfMouseButtons);
    }
}
