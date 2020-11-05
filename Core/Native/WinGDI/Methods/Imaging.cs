using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core.Native
{
    public static partial class WinGdi
    {
        /// <summary>
        ///  Performs a bit-block transfer of the color data corresponding to a rectangle of pixels from the specified source device context into a destination device context.
        /// </summary>
        /// <param name="hdc">A handle to the destination device context.</param>
        /// <param name="x">The x-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
        /// <param name="y">The y-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
        /// <param name="cx">The width, in logical units, of the source and destination rectangles.</param>
        /// <param name="cy">The height, in logical units, of the source and the destination rectangles.</param>
        /// <param name="hdcSrc">A handle to the source device context.</param>
        /// <param name="x1">The x-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
        /// <param name="y1">The y-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
        /// <param name="rop">A raster-operation code. These codes define how the color data for the source rectangle is to be combined with the color data for the destination rectangle to achieve the final color.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("gdi32.dll")] public static extern bool BitBlt(IntPtr hdc, int x, int y, int cx, int cy, IntPtr hdcSrc, int x1, int y1, int rop);

        /// <summary>
        ///  Creates a bitmap compatible with the device that is associated with the specified device context.
        /// </summary>
        /// <param name="hdc">A handle to a device context.</param>
        /// <param name="cx">The bitmap width, in pixels.</param>
        /// <param name="cy">The bitmap height, in pixels.</param>
        /// <returns>If the function succeeds, the return value is a handle to the compatible bitmap (DDB). If the function fails, the return value is NULL.</returns>
        [DllImport("gdi32.dll")] public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int cx, int cy);
    }
}
