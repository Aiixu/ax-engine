namespace Ax.Engine.Core.Rendering
{
    public struct Color : ISurfaceItem
    {
        public int Argb { get; private set; }

        public readonly byte alpha;
        public readonly byte red;
        public readonly byte green;
        public readonly byte blue;

        public Color(byte alpha, byte red, byte green, byte blue) : this()
        {
            this.alpha = alpha;
            this.red = red;
            this.green = green;
            this.blue = blue;

            Argb = alpha << 24 | red << 16 | green << 8 | blue;
        }
    }
}
