using System.Text;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        [DllImport("kernel32", SetLastError = true)] public static extern bool AddConsoleAlias(string Source, string Target, string ExeName);
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetConsoleAlias(string Source, out StringBuilder TargetBuffer, uint TargetBufferLength, string ExeName);
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleAliases(StringBuilder[] lpTargetBuffer, uint targetBufferLength, string lpExeName);
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleAliasesLength(string ExeName);
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleAliasExes(out StringBuilder ExeNameBuffer, uint ExeNameBufferLength);
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleAliasExesLength();
    }
}
