using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {        
        /// TODO : GetSystemMenu

        /// <summary>
        ///  Represents an invalid handle.
        /// </summary>
        public static readonly IntPtr INVALID_HANDLE = new IntPtr(-1);

        /// <summary>
        ///  Retrieves the window handle used by the console associated with the calling process.
        /// </summary>
        /// <returns>The return value is a handle to the window used by the console associated with the calling process or <see langword="null"/> if there is no such associated console.</returns>
        [DllImport("kernel32.dll")] public static extern IntPtr GetConsoleWindow();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="bRevert"></param>
        /// <returns></returns>
        [DllImport("user32.dll")] public static extern IntPtr GetSystemMenu([In] IntPtr hWnd, [In] bool bRevert);

        /// <summary>
        ///  Retrieves a handle to the specified standard device (standard input, standard output, or standard error).
        /// </summary>
        /// <param name="nStdHandle">The standard device. See <see cref="HANDLE"/>.</param>
        /// <returns>
        ///  If the function succeeds, the return value is a handle to the specified device, or a redirected handle set by a previous call to <see cref="SetStdHandle(uint, IntPtr)"/>. The handle has <see cref="BUFFER_ACCESS_MODE.GENERIC_READ"/> and <see cref="BUFFER_ACCESS_MODE.GENERIC_WRITE"/> access rights, unless the application has used <see cref="SetStdHandle(uint, IntPtr)"/> to set a standard handle with lesser access.
        ///  If the function fails, the return value is <see cref="INVALID_HANDLE"/>.
        ///  If an application does not have associated standard handles, such as a service running on an interactive desktop, and has not redirected them, the return value is NULL.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern IntPtr GetStdHandle([In] uint nStdHandle);

        /// <summary>
        ///  Sets the handle for the specified standard device (standard input, standard output, or standard error).
        /// </summary>
        /// <param name="nStdHandle">The standard device for which the handle is to be set. This parameter can be one of the following values.</param>
        /// <param name="hHandle">The handle for the standard device.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetStdHandle([In] uint nStdHandle, [In] IntPtr hHandle);
    }
}