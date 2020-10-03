using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Sets the current size and position of a console screen buffer's window.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_READ"/> access right.</param>
        /// <param name="bAbsolute">If this parameter is TRUE, the coordinates specify the new upper-left and lower-right corners of the window. If it is FALSE, the coordinates are relative to the current window-corner coordinates.</param>
        /// <param name="lpConsoleWindow">A pointer to a <see cref="SMALL_RECT"/> structure that specifies the new upper-left and lower-right corners of the window.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleWindowInfo([In] IntPtr hConsoleOutput, [In] bool bAbsolute, [In] ref SMALL_RECT lpConsoleWindow);

        /// <summary>
        ///  Enables the application to access the window menu (also known as the system menu or the control menu) for copying and modifying.
        /// </summary>
        /// <param name="hWnd">A handle to the window that will own a copy of the window menu.</param>
        /// <param name="bRevert">The action to be taken. If this parameter is FALSE, <see cref="GetSystemMenu(IntPtr, bool)"/> returns a handle to the copy of the window menu currently in use. The copy is initially identical to the window menu, but it can be modified. If this parameter is TRUE, <see cref="GetSystemMenu(IntPtr, bool)"/> resets the window menu back to the default state. The previous window menu, if any, is destroyed.</param>
        /// <returns>If the <paramref name="bRevert"/> parameter is FALSE, the return value is a handle to a copy of the window menu. If the <paramref name="bRevert"/> parameter is TRUE, the return value is NULL.</returns>
        [DllImport("user32.dll")] public static extern IntPtr GetSystemMenu([In] IntPtr hWnd, [In] bool bRevert);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="nPosition"></param>
        /// <param name="wFlags"></param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("user32.dll")] public static extern bool DeleteMenu([In] IntPtr hMenu, [In] int nPosition, [In] int wFlags);

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
        [DllImport("user32.dll")] public static extern bool SetWindowPos([In] IntPtr hWnd, [In] IntPtr hWndInsertAfter, [In] int X, [In] int Y, [In] int cx, [In] int cy, [In] uint flags);
    }
}
