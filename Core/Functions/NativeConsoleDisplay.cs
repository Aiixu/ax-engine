using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Retrieves the size of the largest possible console window, based on the current font and the size of the display.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer.</param>
        /// <returns>If the function succeeds, the return value is a <see cref="COORD"/> structure that specifies the number of character cell columns (X member) and rows (Y member) in the largest possible console window. Otherwise, the members of the structure are zero.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern COORD GetLargestConsoleWindowSize(IntPtr hConsoleOutput);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="nPosition"></param>
        /// <param name="wFlags"></param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("user32.dll")] public static extern bool DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="flags"></param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("user32.dll")] public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint flags);
    }
}
