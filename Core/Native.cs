using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        // https://www.pinvoke.net/default.aspx/kernel32/ConsoleFunctions.html

        [DllImport("kernel32", SetLastError = true)] public static extern bool AllocConsole();
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool AttachConsole(uint dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)] public static extern bool FreeConsole();
    }
}
