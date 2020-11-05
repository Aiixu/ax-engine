using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core.Native
{
    public static partial class WinUser
    {
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

        /// <summary>
        ///  Retrieves the device context (DC) for the entire window, including title bar, menus, and scroll bars. A window device context permits painting anywhere in a window, because the origin of the device context is the upper-left corner of the window instead of the client area. GetWindowDC assigns default attributes to the window device context each time it retrieves the device context.Previous attributes are lost.
        /// </summary>
        /// <param name="hWnd">A handle to the window with a device context that is to be retrieved. If this value is NULL, GetWindowDC retrieves the device context for the entire screen.</param>
        /// <returns>If the function succeeds, the return value is a handle to a device context for the specified window. If the function fails, the return value is NULL, indicating an error or an invalid hWnd parameter.</returns>
        [DllImport("user32.dll")] public static extern IntPtr GetWindowDC(IntPtr hWnd);

        /// <summary>
        ///  Releases a device context (DC), freeing it for use by other applications. The effect of the ReleaseDC function depends on the type of DC. It frees only common and window DCs. It has no effect on class or private DCs.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose DC is to be released.</param>
        /// <param name="hDC">A handle to the DC to be released.</param>
        /// <returns>The return value indicates whether the DC was released. If the DC was released, the return value is 1. If the DC was not released, the return value is zero.</returns>
        [DllImport("user32.dll")] public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>
        ///  Retrieves the dimensions of the bounding rectangle of the specified window. The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpRect">A pointer to a <see cref="RECT"/> structure that receives the screen coordinates of the upper-left and lower-right corners of the window.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("user32.dll")] public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        /// <summary>
        ///  Retrieves a handle to the desktop window. The desktop window covers the entire screen. The desktop window is the area on top of which other windows are painted.
        /// </summary>
        /// <returns>A handle to the desktop window.</returns>
        [DllImport("user32.dll")] public static extern IntPtr GetDesktopWindow();
    }
}
