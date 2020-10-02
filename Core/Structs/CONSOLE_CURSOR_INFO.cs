using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Contains information about the console cursor.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct CONSOLE_CURSOR_INFO
        {
            /// <summary>
            ///  The percentage of the character cell that is filled by the cursor. This value is between 1 and 100. The cursor appearance varies, ranging from completely filling the cell to showing up as a horizontal line at the bottom of the cell.
            /// </summary>
            /// <remarks>
            ///  While the dwSize value is normally between 1 and 100, under some circumstances a value outside of that range might be returned. For example, if CursorSize is set to 0 in the registry, the dwSize value returned would be 0.
            /// </remarks>
            public uint Size;

            /// <summary>
            ///  The visibility of the cursor. If the cursor is visible, this member is TRUE.
            /// </summary>
            public bool Visible;
        }
    }
}
