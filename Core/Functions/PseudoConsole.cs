using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Closes a pseudoconsole from the given handle.
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)] public static extern void ClosePseudoConsole([In] IntPtr hPC);

        /// <summary>
        ///  Creates a new pseudoconsole object for the calling process.
        /// </summary>
        /// <param name="size">The dimensions of the window/buffer in count of characters that will be used on initial creation of the pseudoconsole. This can be adjusted later with <see cref="ResizePseudoConsole(IntPtr, COORD)"/>.</param>
        /// <param name="hInput">An open handle to a stream of data that represents user input to the device. This is currently restricted to synchronous I/O.</param>
        /// <param name="hOutput">An open handle to a stream of data that represents application output from the device. This is currently restricted to synchronous I/O.</param>
        /// <param name="dwFlags">Instantiation flags.</param>
        /// <param name="phPC">Pointer to a location that will receive a handle to the new pseudoconsole device.</param>
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)] public static extern void CreatePseudoConsole([In] COORD size, [In] IntPtr hInput, [In] IntPtr hOutput, [In] short dwFlags, [Out] IntPtr phPC);

        /// <summary>
        ///  Resizes the internal buffers for a pseudoconsole to the given size.
        /// </summary>
        /// <param name="hPC">A handle to an active psuedoconsole as opened by <see cref="CreatePseudoConsole(COORD, IntPtr, IntPtr, short, IntPtr)"/>.</param>
        /// <param name="size">The dimensions of the window/buffer in count of characters that will be used for the internal buffer of this pseudoconsole.</param>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern void ResizePseudoConsole([In] IntPtr hPC, [In] COORD size);
    }
}
