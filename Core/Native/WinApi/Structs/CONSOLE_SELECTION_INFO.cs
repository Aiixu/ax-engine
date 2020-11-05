using System.Runtime.InteropServices;

namespace Ax.Engine.Core.Native
{
    public static partial class WinApi
    {
        /// <summary>
        ///  Contains information for a console selection.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct CONSOLE_SELECTION_INFO
        {
            /// <summary>
            ///  The selection indicator. <see cref="CONSOLE_SELECTION_INFO_FLAGS"/>
            /// </summary>
            public uint dwFlags;

            /// <summary>
            ///  A <see cref="COORD"/> structure that specifies the selection anchor, in characters.
            /// </summary>
            public COORD dwSelectionAnchor;

            /// <summary>
            ///  A <see cref="SMALL_RECT"/> structure that specifies the selection rectangle.
            /// </summary>
            public SMALL_RECT srSelection;
        }
    }
}
