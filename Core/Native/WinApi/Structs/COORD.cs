using System.Runtime.InteropServices;

namespace Ax.Engine.Core.Native
{
    public static partial class WinApi
    {
        /// <summary>
        ///  Defines the coordinates of a character cell in a console screen buffer. The origin of the coordinate system (0,0) is at the top, left cell of the buffer.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            /// <summary>
            ///  The horizontal coordinate or column value. The units depend on the function call.
            /// </summary>
            public short X;

            /// <summary>
            ///  The vertical coordinate or row value. The units depend on the function call.
            /// </summary>
            public short Y;

            public COORD(short x, short y)
            {
                X = x;
                Y = y;
            }

            public override string ToString()
            {
                return $"(X:{X},Y:{Y})";
            }
        }
    }
}
