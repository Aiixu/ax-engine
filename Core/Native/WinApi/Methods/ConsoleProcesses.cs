using System.Runtime.InteropServices;

namespace Ax.Engine.Core.Native
{
    public static partial class WinApi
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
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool AttachConsole([In] uint dwProcessId);

        /// <summary>
        ///  Detaches the calling process from its console.
        /// </summary>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)] public static extern bool FreeConsole();

        /// <summary>
        ///  Retrieves the input code page used by the console associated with the calling process. A console uses its input code page to translate keyboard input into the corresponding character value.
        /// </summary>
        /// <returns>The return value is a code that identifies the code page.</returns>
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
        /// <returns>
        ///  If the function succeeds, the return value is less than or equal to <paramref name="lpdwProcessList"/> and represents the number of process identifiers stored in the <paramref name="lpdwProcessList"/> buffer.
        ///  If the buffer is too small to hold all the valid process identifiers, the return value is the required number of array elements. The function will have stored no identifiers in the buffer. In this situation, use the return value to allocate a buffer that is large enough to store the entire list and call the function again.
        ///  If the return value is zero, the function has failed, because every console has at least one process associated with it.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleProcessList([Out] out uint[] lpdwProcessList, [In] uint dwProcessCount);

        /// <summary>
        ///  Retrieves the output code page used by the console associated with the calling process. A console uses its output code page to translate the character values written by the various output functions into the images displayed in the console window.
        /// </summary>
        /// <returns>The return value is a code that identifies the code page.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleOutputCP();

        /// <summary>
        ///  Sets the output code page used by the console associated with the calling process. A console uses its output code page to translate the character values written by the various output functions into the images displayed in the console window.
        /// </summary>
        /// <param name="wCodePageID">The identifier of the code page to set.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleOutputCP([In] uint wCodePageID);
    }
}
