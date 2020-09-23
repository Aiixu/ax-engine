namespace Ax.Engine.Core
{
    public static partial class Native
    {
        public struct WINDOW_BUFFER_SIZE_RECORD
        {
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
