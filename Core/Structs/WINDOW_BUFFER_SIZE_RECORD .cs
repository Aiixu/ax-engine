namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Describes a change in the size of the console screen buffer.
        /// </summary>
        public struct WINDOW_BUFFER_SIZE_RECORD
        {
            /// <summary>
            ///  A <see cref="COORD"/> structure that contains the size of the console screen buffer, in character cell columns and rows.
            /// </summary>
            public COORD dwSize;

            public WINDOW_BUFFER_SIZE_RECORD(short x, short y)
            {
                dwSize = new COORD
                {
                    X = x,
                    Y = y
                };
            }
        }
    }
}
