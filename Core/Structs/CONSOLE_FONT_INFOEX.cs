using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Contains extended information for a console font.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CONSOLE_FONT_INFOEX
        {
            /// <summary>
            ///  The size of this structure, in bytes.
            /// </summary>
            public uint cbSize;

            /// <summary>
            ///  The index of the font in the system's console font table.
            /// </summary>
            public uint nFont;

            /// <summary>
            ///  A <see cref="COORD"/> structure that contains the width and height of each character in the font, in logical units. The X member contains the width, while the Y member contains the height.
            /// </summary>
            public COORD dwFontSize;

            /// <summary>
            ///  The font pitch and family. For information about the possible values for this member, see <see cref="TEXTMETRIC.tmPitchAndFamily"/>.
            /// </summary>
            public int FontFamily;

            /// <summary>
            ///  The font weight. The weight can range from 100 to 1000, in multiples of 100. For example, the normal weight is 400, while 700 is bold.
            /// </summary>
            public int FontWeight;

            /// <summary>
            ///  The name of the typeface
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string FaceName;
        }
    }
}
