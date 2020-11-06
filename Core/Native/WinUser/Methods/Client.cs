using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core.Native
{
    public static partial class WinUser
    {
        /// <summary>
        ///  Retrieves the coordinates of a window's client area. The client coordinates specify the upper-left and lower-right corners of the client area. Because client coordinates are relative to the upper-left corner of a window's client area, the coordinates of the upper-left corner are (0,0).
        /// </summary>
        /// <param name="hWnd">A handle to the window whose client coordinates are to be retrieved.</param>
        /// <param name="lpRect">A pointer to a <see cref="RECT"/> structure that receives the client coordinates. The left and top members are zero. The right and bottom members contain the width and height of the window.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("user32.dll")] public static extern bool GetClientRect([In] IntPtr hWnd, [In, Out] ref RECT lpRect);
    }
}
