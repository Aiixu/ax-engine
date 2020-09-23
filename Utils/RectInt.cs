namespace Ax.Engine.Utils
{
    public struct RectInt
    {
        public Vector2Int position;
        public Vector2Int size;

        public int X
        {
            get => position.x;
            set => position.x = value;
        }

        public int Y
        {
            get => position.y;
            set => position.y = value;
        }

        public int Width
        {
            get => size.x;
            set => size.x = value;
        }

        public int Height
        {
            get => size.y;
            set => size.y = value;
        }

        public int Top { get => Y; }
        public int Right { get => X + Width; }
        public int Bottom { get => Y + Height; }
        public int Left { get => X; }

        public RectInt(Vector2Int position, Vector2Int size)
        {
            this.position = position;
            this.size = size;
        }

        public RectInt(int x, int y, int width, int height)
        {
            position = new Vector2Int(x, y);
            size = new Vector2Int(width, height);
        }
    }
}
