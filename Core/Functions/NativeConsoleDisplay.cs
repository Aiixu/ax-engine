using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleTitle([Out] StringBuilder lpConsoleTitle, uint nSize);
        [DllImport("kernel32.dll", SetLastError = true)] public static extern uint GetConsoleOriginalTitle(out StringBuilder ConsoleTitle, uint Size);
        [DllImport("kernel32.dll")] public static extern bool SetConsoleTitle(string lpConsoleTitle);

        [DllImport("kernel32.dll", SetLastError = true)] public static extern COORD GetLargestConsoleWindowSize(IntPtr hConsoleOutput);

        [DllImport("user32.dll")] public static extern bool DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")] public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint flags);
    }
}
