using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        public const int FIXED_WIDTH_TRUETYPE = 54;

        [DllImport("kernel32.dll", SetLastError = true)] public static extern int SetCurrentConsoleFontEx(IntPtr ConsoleOutput, bool MaximumWindow, ref CONSOLE_FONT_INFO_EX ConsoleCurrentFontEx);
        [DllImport("kernel32.dll", SetLastError = true)] public static extern COORD GetConsoleFontSize(IntPtr hConsoleOutput, int nFont);
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)] public static extern bool GetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool bMaximumWindow, ref CONSOLE_FONT_INFO_EX lpConsoleCurrentFont);
    }
}
