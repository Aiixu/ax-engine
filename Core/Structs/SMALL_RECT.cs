namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Defines the coordinates of the upper left and lower right corners of a rectangle.
        /// </summary>
        public struct SMALL_RECT
        {
            /// <summary>
            ///  The x-coordinate of the upper left corner of the rectangle.
            /// </summary>
            public short Left;

            /// <summary>
            ///  The y-coordinate of the upper left corner of the rectangle.
            /// </summary>
            public short Top;

            /// <summary>
            ///  The x-coordinate of the lower right corner of the rectangle.
            /// </summary>
            public short Right;

            /// <summary>
            ///  The y-coordinate of the lower right corner of the rectangle.
            /// </summary>
            public short Bottom;
        }
    }
}