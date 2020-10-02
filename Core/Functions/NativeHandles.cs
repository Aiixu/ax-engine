using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        public const uint STD_OUTPUT_HANDLE = unchecked((uint)-11);
        public const uint STD_INPUT_HANDLE = unchecked((uint)-10);

        public static readonly IntPtr INVALID_HANDLE = new IntPtr(-1);

        [DllImport("kernel32.dll")] public static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")] public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("kernel32.dll", SetLastError = true)] public static extern IntPtr GetStdHandle(uint nStdHandle);
        [DllImport("kernel32.dll", SetLastError = true)] public static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetStdHandle(uint nStdHandle, IntPtr hHandle);
    }
}