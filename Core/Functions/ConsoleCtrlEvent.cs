using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Describes a callback for a control CTRL event.
        /// </summary>
        /// <param name="CtrlType"></param>
        /// <returns></returns>
        public delegate bool ConsoleCtrlDelegate(CTRL_TYPES CtrlType);

        /// <summary>
        ///  Sends a specified signal to a console process group that shares the console associated with the calling process.
        /// </summary>
        /// <param name="dwCtrlEvent">The type of signal to be generated. See <see cref="CTRL_TYPES"/></param>
        /// <param name="dwProcessGroupId">The identifier of the process group to receive the signal. A process group is created when the CREATE_NEW_PROCESS_GROUP flag is specified in a call to the CreateProcess function. The process identifier of the new process is also the process group identifier of a new process group. The process group includes all processes that are descendants of the root process. Only those processes in the group that share the same console as the calling process receive the signal. In other words, if a process in the group creates a new console, that process does not receive the signal, nor do its descendants. <br></br>If this parameter is zero, the signal is generated in all processes that share the console of the calling process.</param>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);
    }
}