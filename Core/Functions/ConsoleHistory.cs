using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Retrieves the history settings for the calling process's console.
        /// </summary>
        /// <param name="ConsoleHistoryInfo">A handle to the console screen buffer. The handle must have the GENERIC_READ access right.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetConsoleHistoryInfo(out CONSOLE_HISTORY_INFO ConsoleHistoryInfo);
    }
}
