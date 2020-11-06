using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core.Native
{
    public static partial class WinGdi
    {
        /// <summary>
        ///   Creates a memory device context (DC) compatible with the specified device.
        /// </summary>
        /// <param name="hdc">A handle to an existing DC. If this handle is NULL, the function creates a memory DC compatible with the application's current screen.</param>
        /// <returns>If the function succeeds, the return value is the handle to a memory DC. If the function fails, the return value is <see langword="null"/>.</returns>
        [DllImport("gdi32.dll", SetLastError = true)] public static extern IntPtr CreateCompatibleDC([In] IntPtr hdc);

        /// <summary>
        ///  Deletes the specified device context (DC).
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("gdi32.dll", SetLastError = true)] public static extern bool DeleteDC([In] IntPtr hdc);

        /// <summary>
        ///  Deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. After the object is deleted, the specified handle is no longer valid.
        /// </summary>
        /// <param name="ho">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("gdi32.dll", SetLastError = true)] public static extern bool DeleteObject([In] IntPtr ho);

        /// <summary>
        ///  Selects an object into the specified device context (DC). The new object replaces the previous object of the same type.
        /// </summary>
        /// <param name="hdc">A handle to the DC.</param>
        /// <param name="h">A handle to the object to be selected.</param>
        /// <returns>If the selected object is not a region and the function succeeds, the return value is a handle to the object being replaced.</returns>
        [DllImport("gdi32.dll", SetLastError = true)] public static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr h);
    }
}
