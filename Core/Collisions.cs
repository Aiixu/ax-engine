using Ax.Engine.Utils;

namespace Ax.Engine.Core
{
    public static class Collisions
    {
        public static bool AABBB(RectInt aa, RectInt bb) =>
            aa.Right >= bb.Left &&
            bb.Right >= aa.Left &&
            aa.Bottom >= bb.Top &&
            bb.Bottom >= aa.Top;
    }
}
