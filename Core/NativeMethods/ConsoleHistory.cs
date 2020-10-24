using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Retrieves the history settings for the calling process's console.
        /// </summary>
        /// <param name="ConsoleHistoryInfo">A handle to the console screen buffer. The handle must have the GENERIC_READ access right.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetConsoleHistoryInfo([Out] out CONSOLE_HISTORY_INFO ConsoleHistoryInfo);

        /// <summary>
        ///  Sets the history settings for the calling process's console.
        /// </summary>
        /// <param name="ConsoleHistoryInfo">A pointer to a <see cref="CONSOLE_HISTORY_INFO"/> structure that contains the history settings for the process's console.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleHistoryInfo([In] CONSOLE_HISTORY_INFO ConsoleHistoryInfo);
    }
}
