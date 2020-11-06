using System;
using System.Drawing;
using System.Net.Http;

namespace Ax.Engine.Core.Rendering
{
    public struct RgbSurfaceItem : ISurfaceItem
    {
        public byte[] Bytes { get; }
        public readonly int zIndex;

        public static readonly byte[] BaseColorSequence = new byte[]
        {
            27, 91, 0, 56, 59, 50, 59, 48, 48, 48, 59, 48, 48, 48, 59, 48, 48, 48, 109
        };

        public RgbSurfaceItem(Color color)
            : this(color.R, color.G, color.B)
        { }

        public RgbSurfaceItem(byte red, byte green, byte blue)
        {
            // TODO -> set
            zIndex = 0;

            Bytes = new byte[19];
            Buffer.BlockCopy(BaseColorSequence, 0, Bytes, 0, 19);

            // red
            Bytes[7] = (byte)(red / 100 % 10 + 48);
            Bytes[8] = (byte)(red / 010 % 10 + 48);
            Bytes[9] = (byte)(red / 001 % 10 + 48);
            
            // green
            Bytes[11] = (byte)(green / 100 % 10 + 48);
            Bytes[12] = (byte)(green / 010 % 10 + 48);
            Bytes[13] = (byte)(green / 001 % 10 + 48);
            
            // blue              
            Bytes[15] = (byte)(blue / 100 % 10 + 48);
            Bytes[16] = (byte)(blue / 010 % 10 + 48);
            Bytes[17] = (byte)(blue / 001 % 10 + 48);
        }
    }
}
