using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Contains information for a console font.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct CONSOLE_FONT_INFO
        {
            /// <summary>
            ///  The index of the font in the system's console font table.
            /// </summary>
            public int nFont;

            /// <summary>
            ///  A <see cref="COORD"/> structure that contains the width and height of each character in the font, in logical units. The X member contains the width, while the Y member contains the height.
            /// </summary>
            public COORD dwFontSize;
        }
    }
}