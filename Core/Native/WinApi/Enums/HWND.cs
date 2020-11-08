using System;

namespace Ax.Engine.Core.Native
{
    public static partial class WinApi
    {
        /// <summary>
        ///  Places the window at the bottom of the Z order. If the <i>hWnd</i> parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
        /// </summary>
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        /// <summary>
        ///  Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
        /// </summary>
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        /// <summary>
        ///  Places the window at the top of the Z order.
        /// </summary>
        public static readonly IntPtr HWND_TOP = IntPtr.Zero;

        /// <summary>
        ///  Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
        /// </summary>
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    }
}
