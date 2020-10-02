using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Allocates a new console for the calling process.
        /// </summary>
        [DllImport("kernel32", SetLastError = true)] public static extern bool AllocConsole();

        /// <summary>
        ///  Attaches the calling process to the console of the specified process.
        /// </summary>
        /// <param name="dwProcessId">The identifier of the process whose console is to be used.</param>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool AttachConsole(uint dwProcessId);

        /// <summary>
        ///  Detaches the calling process from its console.
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)] public static extern bool FreeConsole();

        /// <summary>
        ///  Retrieves the input code page used by the console associated with the calling process. A console uses its input code page to translate keyboard input into the corresponding character value.
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleCP();


        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleCP(uint wCodePageID);


        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleProcessList(out uint[] ProcessList, uint ProcessCount);


        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleOutputCP();


        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleOutputCP(uint wCodePageID);
    }
}
