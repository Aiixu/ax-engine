namespace Ax.Engine.Utils
{
    public struct Vector2
    {
        public static Vector2 Zero => new Vector2(0);
        public static Vector2 One => new Vector2(1);
        public static Vector2 MinusOne => new Vector2(-1);

        public static Vector2 Top => new Vector2(0, 1);
        public static Vector2 Right => new Vector2(1, 0);
        public static Vector2 Bottom => new Vector2(0, -1);
        public static Vector2 Left => new Vector2(-1, 0);

        public float x;
        public float y;

        public Vector2(float xy) : this(xy, xy)
        { }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 Add(Vector2 b)
        {
            x += b.x;
            x += b.y;
            return this;
        }

        public Vector2 Sub(Vector2 b)
        {
            x -= b.x;
            y -= b.y;
            return this;
        }

        public Vector2 Mul(Vector2 b)
        {
            x *= b.x;
            y *= b.y;
            return this;
        }

        public Vector2 Div(Vector2 b)
        {
            x /= b.x;
            y /= b.y;
            return this;
        }

        public Vector2 Mul(float l) => Mul(new Vector2(l));
        public Vector2 Div(float l) => Div(new Vector2(l));

        public static Vector2 operator +(Vector2 a, Vector2 b) => a.Add(b);
        public static Vector2 operator -(Vector2 a, Vector2 b) => a.Sub(b);
        public static Vector2 operator *(Vector2 a, Vector2 b) => a.Mul(b);
        public static Vector2 operator /(Vector2 a, Vector2 b) => a.Div(b);

        public static Vector2 operator *(Vector2 a, int l) => a.Mul(l);
        public static Vector2 operator /(Vector2 a, int l) => a.Div(l);

        public static bool operator ==(Vector2 a, Vector2 b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(Vector2 a, Vector2 b) => a.x != b.x || a.y != b.y;

        public static explicit operator Vector2(Vector2Int a) => new Vector2(a.x, a.y);

        public override string ToString()
        {
            return $"({x}, {y})";
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() << 2;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this == (Vector2)obj;
        }
    }
}
