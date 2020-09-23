using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleCP();
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleCP(uint wCodePageID);
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleProcessList(out uint[] ProcessList, uint ProcessCount);

        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleOutputCP();
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleOutputCP(uint wCodePageID);
    }
}
