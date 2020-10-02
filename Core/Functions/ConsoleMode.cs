using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Retrieves the current input mode of a console's input buffer or the current output mode of a console screen buffer.
        /// </summary>
        /// <param name="hConsoleHandle">A handle to the console input buffer or the console screen buffer. The handle must have the GENERIC_READ access right.</param>
        /// <param name="lpMode">A pointer to a variable that receives the current mode of the specified buffer.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        /// <summary>
        ///  Sets the input mode of a console's input buffer or the output mode of a console screen buffer.
        /// </summary>
        /// <param name="hConsoleHandle">A handle to the console input buffer or a console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_READ"/> access right.</param>
        /// <param name="dwMode">The input or output mode to be set. If the <paramref name="hConsoleHandle"/> parameter is an input handle, the mode can be one or more of the following values. When a console is created, all input modes except <see cref="CONSOLE_MODE_INPUT.ENABLE_WINDOW_INPUT"/> are enabled by default. See <see cref="CONSOLE_MODE_INPUT"/> and <see cref="CONSOLE_MODE_OUTPUT"/>.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleMode([In] IntPtr hConsoleHandle, [In] uint dwMode);
    }
}
