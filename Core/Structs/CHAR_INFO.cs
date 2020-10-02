using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        /// Specifies a Unicode or ANSI character and its attributes.
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct CHAR_INFO
        {
            /// <summary>
            /// Unicode character of a screen buffer character cell.
            /// </summary>
            [FieldOffset(0)] public char UnicodeChar;

            /// <summary>
            /// ANSI character of a screen buffer character cell.
            /// </summary>
            [FieldOffset(0)] public char AsciiChar;

            /// <summary>
            /// The character attributes. See <see cref="CHAR_INFO_ATTRIBUTE"/>
            /// </summary>
            [FieldOffset(2)] public ushort Attributes;
        }
    }
}
