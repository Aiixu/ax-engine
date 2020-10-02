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
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool FlushConsoleInputBuffer(IntPtr hConsoleInput);
    }
}
