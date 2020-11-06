using System;
using System.Drawing;
using System.Drawing.Imaging;

using static Ax.Engine.Core.Native.WinGdi;
using static Ax.Engine.Core.Native.WinUser;

namespace Ax.Engine.Core
{
    public static class Imaging
    {
        /// <summary>
        ///  Creates an Image object containing a screen shot of the entire desktop
        /// </summary>
        /// <returns>An Image containing a screen shot of the entire desktop</returns>
        public static Image CaptureScreen()
        {
            return CaptureWindow(GetDesktopWindow());
        }

        /// <summary>
        ///  Creates an Image object containing a screen shot of the entire desktop
        /// </summary>
        /// <returns>An Image containing a screen shot of the entire desktop</returns>
        public static Image CaptureScreenToFile()
        {
            return CaptureWindow(GetDesktopWindow());
        }

        /// <summary>
        ///  Captures a screen shot of the entire desktop, and saves it to a file
        /// </summary>
        /// <param name="filename">Target file name</param>
        public static void CaptureScreenToFile(string filename)
        {
            Image img = CaptureScreen();
            img.Save(filename);
        }

        /// <summary>
        ///  Captures a screen shot of the entire desktop, and saves it to a file
        /// </summary>
        /// <param name="filename">Target file name</param>
        /// <param name="format">Format of the image</param>
        public static void CaptureScreenToFile(string filename, ImageFormat format)
        {
            Image img = CaptureScreen();
            img.Save(filename, format);
        }

        /// <summary>
        ///  Creates an Image object containing a screen shot of a specific window
        /// </summary>
        /// <param name="handle">The handle to the window.</param>
        /// <returns>An Image containing a screen shot of the target window</returns>
        public static Image CaptureWindow(IntPtr handle)
        {
            IntPtr hdcSrc = GetWindowDC(handle);

            RECT windowRect = new RECT();
            GetClientRect(handle, ref windowRect);

            int titleBarHeight = (GetSystemMetrics((int)SM.CYFRAME) + GetSystemMetrics((int)SM.CYCAPTION) + GetSystemMetrics((int)SM.CXPADDEDBORDER));

            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top - titleBarHeight;

            IntPtr hdcDest = CreateCompatibleDC(hdcSrc);
            IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, width, height);

            IntPtr hOld = SelectObject(hdcDest, hBitmap);

            BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 8, 1 + titleBarHeight, (int)BITBLT_OP.SRCCOPY);

            SelectObject(hdcDest, hOld);
            DeleteDC(hdcDest);
            ReleaseDC(handle, hdcSrc);

            Image img = Image.FromHbitmap(hBitmap);
            DeleteObject(hBitmap);

            return img;
        }

        /// <summary>
        ///  Captures a screen shot of a specific window, and saves it to a file
        /// </summary>
        /// <param name="handle">The handle to the window.</param>
        /// <param name="filename">Target file name</param>
        public static void CaptureWindowToFile(IntPtr handle, string filename)
        {
            Image img = CaptureWindow(handle);
            img.Save(filename);
        }

        /// <summary>
        ///  Captures a screen shot of a specific window, and saves it to a file
        /// </summary>
        /// <param name="handle">The handle to the window.</param>
        /// <param name="filename">Target file name</param>
        /// <param name="format">Format of the image</param>
        public static void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
        {
            Image img = CaptureWindow(handle);
            img.Save(filename, format);
        }
    }
}
