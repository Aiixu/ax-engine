using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Allocates a new console for the calling process.
        /// </summary>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32", SetLastError = true)] public static extern bool AllocConsole();

        /// <summary>
        ///  Attaches the calling process to the console of the specified process.
        /// </summary>
        /// <param name="dwProcessId">The identifier of the process whose console is to be used.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool AttachConsole(uint dwProcessId);

        /// <summary>
        ///  Detaches the calling process from its console.
        /// </summary>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)] public static extern bool FreeConsole();

        /// <summary>
        ///  Retrieves the input code page used by the console associated with the calling process. A console uses its input code page to translate keyboard input into the corresponding character value.
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleCP();

        /// <summary>
        ///  Sets the input code page used by the console associated with the calling process. A console uses its input code page to translate keyboard input into the corresponding character value.
        /// </summary>
        /// <param name="wCodePageID">The identifier of the code page to be set.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleCP([In] uint wCodePageID);

        /// <summary>
        ///  Retrieves a list of the processes attached to the current console.
        /// </summary>
        /// <param name="lpdwProcessList">A pointer to a buffer that receives an array of process identifiers upon success. This must be a valid buffer and cannot be <see langword="null"/>. The buffer must have space to receive at least 1 returned process id.</param>
        /// <param name="dwProcessCount">The maximum number of process identifiers that can be stored in the <paramref name="lpdwProcessList"/> buffer. This must be greater than 0.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleProcessList(out uint[] lpdwProcessList, uint dwProcessCount);

        /// <summary>
        ///  Retrieves the output code page used by the console associated with the calling process. A console uses its output code page to translate the character values written by the various output functions into the images displayed in the console window.
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleOutputCP();

        /// <summary>
        ///  Sets the output code page used by the console associated with the calling process. A console uses its output code page to translate the character values written by the various output functions into the images displayed in the console window.
        /// </summary>
        /// <param name="wCodePageID">The identifier of the code page to set.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleOutputCP([In] uint wCodePageID);
    }
}
