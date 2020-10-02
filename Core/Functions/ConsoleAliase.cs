using System.Runtime.InteropServices;
using System.Text;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        /// Defines a console alias for the specified executable.
        /// </summary>
        /// <param name="Source">The console alias to be mapped to the text specified by <i>Target</i>.</param>
        /// <param name="Target">The text to be substituted for <i>Source</i>. If this parameter is <see langword="null"/>, then the console alias is removed.</param>
        /// <param name="ExeName">The name of the executable file for which the console alias is to be defined.</param>
        /// <returns></returns>
        [DllImport("kernel32", SetLastError = true)] public static extern bool AddConsoleAlias(string Source, string Target, string ExeName);

        /// <summary>
        ///  Retrieves the text for the specified console alias and executable.
        /// </summary>
        /// <param name="lpSource">The console alias whose text is to be retrieved.</param>
        /// <param name="lpTargetBuffer">A pointer to a buffer that receives the text associated with the console alias.</param>
        /// <param name="TargetBufferLength">The size of the buffer pointed to by lpTargetBuffer, in bytes.</param>
        /// <param name="lpExeName">The name of the executable file.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetConsoleAlias(string lpSource, [Out] out StringBuilder lpTargetBuffer, uint TargetBufferLength, string lpExeName);

        /// <summary>
        ///  Retrieves all defined console aliases for the specified executable.
        /// </summary>
        /// <param name="lpAliasBuffer">A pointer to a buffer that receives the aliases. <br></br>The format of the data is as follows: Source1=Target1\0Source2=Target2\0... SourceN=TargetN\0, where N is the number of console aliases defined.</param>
        /// <param name="AliasBufferLength">The size of the buffer pointed to by <paramref name="lpAliasBuffer"/>, in bytes.</param>
        /// <param name="lpExeName">The executable file whose aliases are to be retrieved.</param>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleAliases([Out] StringBuilder[] lpAliasBuffer, uint AliasBufferLength, string lpExeName);

        /// <summary>
        ///  Retrieves the required size for the buffer used by the <see cref="GetConsoleAliases(StringBuilder[], uint, string)"/> function.
        /// </summary>
        /// <param name="ExeName">The name of the executable file whose console aliases are to be retrieved.</param>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleAliasesLength(string ExeName);

        /// <summary>
        ///  Retrieves the names of all executable files with console aliases defined.
        /// </summary>
        /// <param name="lpExeNameBuffer">A pointer to a buffer that receives the names of the executable files.</param>
        /// <param name="ExeNameBufferLength">The size of the buffer pointed to by lpExeNameBuffer, in bytes.</param>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleAliasExes([Out] out StringBuilder lpExeNameBuffer, uint ExeNameBufferLength);

        /// <summary>
        ///  Retrieves the required size for the buffer used by the <see cref="GetConsoleAliasExes(out StringBuilder, uint)"/> function.
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleAliasExesLength();
    }
}