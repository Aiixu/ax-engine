using System;

namespace Ax.Engine.Core.Rendering
{
    public struct RgbSurfaceItem : ISurfaceItem
    {
        public byte[] Bytes { get; }
        public readonly int zIndex;

        private static readonly byte[] BasedColorSequence = new byte[]
        {
            27, 91, 0, 56, 59, 50, 59, 48, 48, 48, 59, 48, 48, 48, 59, 48, 48, 48, 109
        };

        public RgbSurfaceItem(byte red, byte green, byte blue) : this()
        {
            // TODO -> set
            zIndex = 0;

            Bytes = new byte[19];
            Buffer.BlockCopy(BasedColorSequence, 0, Bytes, 0, 19);

            // red
            Bytes[7] = (byte)(red / 100 + 48);
            Bytes[8] = (byte)(red / 010 % 10 + 48);
            Bytes[9] = (byte)(red / 001 % 10 + 48);
            
            // green
            Bytes[11] = (byte)(green / 100 + 48);
            Bytes[12] = (byte)(green / 010 % 10 + 48);
            Bytes[13] = (byte)(green % 10 + 48);
            
            // blue              
            Bytes[15] = (byte)(blue / 100 + 48);
            Bytes[16] = (byte)(blue / 010 % 10 + 48);
            Bytes[17] = (byte)(blue % 10 + 48);
        }
    }
}
