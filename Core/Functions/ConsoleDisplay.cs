using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Retrieves the display mode of the current console.
        /// </summary>
        /// <param name="lpModeFlags">The display mode of the console.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetConsoleDisplayMode(out uint lpModeFlags);
    }
}
