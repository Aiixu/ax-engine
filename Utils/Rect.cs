namespace Ax.Engine.Utils
{
    public struct Rect
    {
        public Vector2 position;
        public Vector2 size;

        public float X 
        {
            get => position.x;
            set => position.x = value;
        }

        public float Y 
        {
            get => position.y;
            set => position.y = value;
        }

        public float Width 
        { 
            get => size.x;
            set => size.x = value;
        }

        public float Height
        { 
            get => size.y;
            set => size.y = value;
        }

        public float Top { get => Y; }
        public float Right { get => X + Width; }
        public float Bottom { get => Y + Height; }
        public float Left { get => X; }

        public Rect(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;
        }

        public Rect(float x, float y, float width, float height)
        {
            position = new Vector2(x, y);
            size = new Vector2(width, height);
        }
    }
}
