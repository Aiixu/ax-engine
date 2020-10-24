using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Retrieves information about the current console selection.
        /// </summary>
        /// <param name="lpConsoleSelectionInfo">A <see cref="CONSOLE_SELECTION_INFO"/> structure that contains the selection information.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetConsoleSelectionInfo([Out] CONSOLE_SELECTION_INFO lpConsoleSelectionInfo);
    }
}
