using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Closes a pseudoconsole from the given handle.
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)] public static extern void ClosePseudoConsole(IntPtr hPC);

        /// TODO : ResizePseudoConsole

        /// <summary>
        /// Creates a new pseudoconsole object for the calling process.
        /// </summary>
        /// <param name="size">The dimensions of the window/buffer in count of characters that will be used on initial creation of the pseudoconsole. This can be adjusted later with ResizePseudoConsole.</param>
        /// <param name="hInput">An open handle to a stream of data that represents user input to the device. This is currently restricted to synchronous I/O.</param>
        /// <param name="hOutput">An open handle to a stream of data that represents application output from the device. This is currently restricted to synchronous I/O.</param>
        /// <param name="dwFlags">Instantiation flags.</param>
        /// <param name="phPC">Pointer to a location that will receive a handle to the new pseudoconsole device.</param>
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)] public static extern void CreatePseudoConsole(COORD size, IntPtr hInput, IntPtr hOutput, short dwFlags, [Out] IntPtr phPC);
    }
}
