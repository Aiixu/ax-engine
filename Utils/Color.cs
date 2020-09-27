using System;
using System.Globalization;

namespace Ax.Engine.Utils
{
    public class Color : IEquatable<Color>
    {
        public byte r;
        public byte g;
        public byte b;

        public Color(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public byte this[int index]
        {
            get => index switch
            {
                0 => r,
                1 => g,
                2 => b,

                _ => throw new IndexOutOfRangeException("Invalid Vector3 index!")
            };

            set
            {
                switch (index)
                {
                    case 0: r = value; break;
                    case 1: g = value; break;
                    case 2: b = value; break;

                    default: throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
        }

        public override string ToString()
        {
            return $"(R:{r},G:{g},B:{b})";
        }

        public static Color Black => new Color(0, 0, 0);
        public static Color White => new Color(255, 255, 255);
        public static Color Red => new Color(255, 0, 0);
        public static Color Green => new Color(0, 255, 0);
        public static Color Blue => new Color(0, 0, 255);

        public static Color FromColor(System.Drawing.Color rgb) => FromRgb(rgb.R, rgb.G, rgb.B);

        public static Color FromRgb(byte r, byte g, byte b) => new Color(r, g, b);
        
        public static Color FromHex(string hex)
        {
            hex = hex.Trim().Replace("#", "");

            byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.AllowHexSpecifier);

            return FromRgb(r, g, b);
        }

        public bool Equals(Color other)
        {
            return other != null && r == other.r && g == other.g && b == other.b;
        }
    }
}
