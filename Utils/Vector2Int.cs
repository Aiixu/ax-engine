using System;
using System.Runtime.CompilerServices;

namespace Ax.Engine.Utils
{
    public struct Vector2Int
    {
        public static Vector2Int Zero => new Vector2Int(0);
        public static Vector2Int One => new Vector2Int(1);
        public static Vector2Int MinusOne => new Vector2Int(-1);
                             
        public static Vector2Int Top => new Vector2Int(0, 1);
        public static Vector2Int Right => new Vector2Int(1, 0);
        public static Vector2Int Bottom => new Vector2Int(0, -1);
        public static Vector2Int Left => new Vector2Int(-1, 0);

        public int x;
        public int y;

        public Vector2Int(int xy) : this(xy, xy)
        { }

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2Int Add(Vector2Int b)
        {
            x += b.x;
            x += b.y;
            return this;
        }

        public Vector2Int Sub(Vector2Int b)
        {
            x -= b.x;
            y -= b.y;
            return this;
        }

        public Vector2Int Mul(Vector2Int b)
        {
            x *= b.x;
            y *= b.y;
            return this;
        }

        public Vector2Int Div(Vector2Int b)
        {
            x /= b.x;
            y /= b.y;
            return this;
        }

        public Vector2Int Mul(int l) => Mul(new Vector2Int(l));
        public Vector2Int Div(int l) => Div(new Vector2Int(l));

        public static Vector2Int operator +(Vector2Int a, Vector2Int b) => a.Add(b);
        public static Vector2Int operator -(Vector2Int a, Vector2Int b) => a.Sub(b);
        public static Vector2Int operator *(Vector2Int a, Vector2Int b) => a.Mul(b);
        public static Vector2Int operator /(Vector2Int a, Vector2Int b) => a.Div(b);

        public static Vector2Int operator *(Vector2Int a, int l) => a.Mul(l);
        public static Vector2Int operator /(Vector2Int a, int l) => a.Div(l);

        public static bool operator ==(Vector2Int a, Vector2Int b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(Vector2Int a, Vector2Int b) => a.x != b.x || a.y != b.y;

        public static explicit operator Vector2Int(Vector2 a) => new Vector2Int((int)a.x, (int)a.y);

        public static Vector2Int Floor(Vector2 v) => new Vector2Int(MathHelper.FloorToInt(v.x), MathHelper.FloorToInt(v.y));

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

            return this == (Vector2Int)obj;
        }
    }
}
