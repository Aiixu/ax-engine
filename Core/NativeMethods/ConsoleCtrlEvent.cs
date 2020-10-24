using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  An application-defined function used with the <see cref="SetConsoleCtrlHandler(HandlerRoutine, bool)"/> function. A console process uses this function to handle control signals received by the process. When the signal is received, the system creates a new thread in the process to execute the function.
        /// </summary>
        /// <param name="CtrlType">The type of control signal received by the handler. </param>
        /// <returns>If the function handles the control signal, it should return TRUE. If it returns FALSE, the next handler function in the list of handlers for this process is used.</returns>
        public delegate bool HandlerRoutine([In] CTRL_TYPES CtrlType);

        /// <summary>
        ///  Sends a specified signal to a console process group that shares the console associated with the calling process.
        /// </summary>
        /// <param name="dwCtrlEvent">The type of signal to be generated. See <see cref="CTRL_TYPES"/></param>
        /// <param name="dwProcessGroupId">
        ///  The identifier of the process group to receive the signal. A process group is created when the CREATE_NEW_PROCESS_GROUP flag is specified in a call to the CreateProcess function. 
        ///  The process identifier of the new process is also the process group identifier of a new process group. The process group includes all processes that are descendants of the root process. Only those processes in the group that share the same console as the calling process receive the signal. In other words, if a process in the group creates a new console, that process does not receive the signal, nor do its descendants. 
        ///  If this parameter is zero, the signal is generated in all processes that share the console of the calling process.
        ///  </param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GenerateConsoleCtrlEvent([In] uint dwCtrlEvent, [In] uint dwProcessGroupId);

        /// <summary>
        ///  Adds or removes an application-defined HandlerRoutine function from the list of handler functions for the calling process.
        ///  If no handler function is specified, the function sets an inheritable attribute that determines whether the calling process ignores CTRL+C signals.
        /// </summary>
        /// <param name="HandlerRoutine">A pointer to the application-defined <see cref="HandlerRoutine"/> function to be added or removed. This parameter can be NULL.</param>
        /// <param name="Add"></param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleCtrlHandler([In, Optional] HandlerRoutine HandlerRoutine, [In] bool Add);
    }
}