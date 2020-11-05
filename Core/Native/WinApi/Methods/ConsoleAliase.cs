using System.Text;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core.Native
{
    public static partial class WinApi
    {
        /// <summary>
        /// Defines a console alias for the specified executable.
        /// </summary>
        /// <param name="Source">The console alias to be mapped to the text specified by <i>Target</i>.</param>
        /// <param name="Target">The text to be substituted for <i>Source</i>. If this parameter is <see langword="null"/>, then the console alias is removed.</param>
        /// <param name="ExeName">The name of the executable file for which the console alias is to be defined.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32", SetLastError = true)] public static extern bool AddConsoleAlias([In] string Source, [In] string Target, [In] string ExeName);

        /// <summary>
        ///  Retrieves the text for the specified console alias and executable.
        /// </summary>
        /// <param name="lpSource">The console alias whose text is to be retrieved.</param>
        /// <param name="lpTargetBuffer">A pointer to a buffer that receives the text associated with the console alias.</param>
        /// <param name="TargetBufferLength">The size of the buffer pointed to by lpTargetBuffer, in bytes.</param>
        /// <param name="lpExeName">The name of the executable file.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetConsoleAlias([In] string lpSource, [Out] out StringBuilder lpTargetBuffer, [In] uint TargetBufferLength, [In] string lpExeName);

        /// <summary>
        ///  Retrieves all defined console aliases for the specified executable.
        /// </summary>
        /// <param name="lpAliasBuffer">A pointer to a buffer that receives the aliases. <br></br>The format of the data is as follows: Source1=Target1\0Source2=Target2\0... SourceN=TargetN\0, where N is the number of console aliases defined.</param>
        /// <param name="AliasBufferLength">The size of the buffer pointed to by <paramref name="lpAliasBuffer"/>, in bytes.</param>
        /// <param name="lpExeName">The executable file whose aliases are to be retrieved.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetConsoleAliases([Out] StringBuilder[] lpAliasBuffer, [In] uint AliasBufferLength, [In] string lpExeName);

        /// <summary>
        ///  Retrieves the required size for the buffer used by the <see cref="GetConsoleAliases(StringBuilder[], uint, string)"/> function.
        /// </summary>
        /// <param name="lpExeName">The name of the executable file whose console aliases are to be retrieved.</param>
        /// <returns>The size of the buffer required to store all console aliases defined for this executable file, in bytes.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleAliasesLength([In] string lpExeName);

        /// <summary>
        ///  Retrieves the names of all executable files with console aliases defined.
        /// </summary>
        /// <param name="lpExeNameBuffer">A pointer to a buffer that receives the names of the executable files.</param>
        /// <param name="ExeNameBufferLength">The size of the buffer pointed to by lpExeNameBuffer, in bytes.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetConsoleAliasExes([Out] out StringBuilder lpExeNameBuffer, [In] uint ExeNameBufferLength);

        /// <summary>
        ///  Retrieves the required size for the buffer used by the <see cref="GetConsoleAliasExes(out StringBuilder, uint)"/> function.
        /// </summary>
        /// <returns>The size of the buffer required to store the names of all executable files that have console aliases defined, in bytes.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetConsoleAliasExesLength();
    }
}