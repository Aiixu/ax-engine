using System.Drawing;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core.Native
{
    public static partial class WinGdi
    {
        /// <summary>
        ///  The COLORREF value is used to specify an RGB color.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct COLORREF
        {
            /// <summary>
            ///  The value of the color, stored in hexadecimal.
            /// </summary>
            public uint ColorDWORD;

            /// <summary>
            ///  Create a <see cref="COLORREF"/> from a <see cref="Color"/>
            /// </summary>
            /// <param name="color"></param>
            public COLORREF(Color color)
            {
                ColorDWORD = color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
            }

            /// <summary>
            ///  Returns a <see cref="Color"/> from a <see cref="COLORREF"/>
            /// </summary>
            public Color GetColor()
            {
                return Color.FromArgb((int)(0x000000FFU & ColorDWORD), (int)(0x0000FF00U & ColorDWORD) >> 8, (int)(0x00FF0000U & ColorDWORD) >> 16);
            }

            /// 
            /// <summary>
            ///  Set the <see cref="ColorDWORD"/> value from a <see cref="Color"/>.
            /// </summary>
            public void SetColor(Color color)
            {
                ColorDWORD = color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
            }
        }
    }
}