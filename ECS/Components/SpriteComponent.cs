using System.Drawing;

using Ax.Engine.Utils;
using Ax.Engine.Core.Rendering;

namespace Ax.Engine.ECS.Components
{
    public sealed class SpriteComponent : Component
    {
        public enum RenderMode
        {
            Simple,
            Resize
        }

        public Bitmap texture;
        public RectInt sourceRect;

        public RenderMode renderMode;

        private RectInt destRect;

        public int Width
        {
            get => sourceRect.Width;
            set => sourceRect.Width = value;
        }

        public int Height
        {
            get => sourceRect.Height;
            set => sourceRect.Height = value;
        }

        public override void Init()
        {
            sourceRect = new RectInt(0, 0, 8, 8);
            destRect = new RectInt(0, 0, 8, 8);
        }

        public override void Update()
        {
            destRect.position = (Vector2Int)Transform.position;
            destRect.size = (Vector2Int)((Vector2)sourceRect.size * Transform.scale);
        }

        public override void Render(SurfaceRenderer renderer)
        {
            for (int y = 0; y < destRect.Height; y++)
            {
                for (int x = 0; x < destRect.Width; x++)
                {
                    Color color = texture.GetPixel(x, y);
                    renderer.Render(new RgbSurfaceItem(color), destRect.X + x, destRect.Y + y);
                }
            }
        }
    }
}
