using System.Drawing;

using Ax.Engine.Core;
using Ax.Engine.Utils;

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

        public SpriteComponent(string path)
        {
            texture = (Bitmap)Image.FromFile(path);
        }

        public override void Init()
        {
            sourceRect = new RectInt(0, 0, 16, 16);
            destRect = new RectInt(0, 0, 16, 16);
        }

        public override void Update()
        {
            destRect.position = (Vector2Int)Transform.position;
            destRect.size = (Vector2Int)((Vector2)sourceRect.size * Transform.scale);
        }

        public override void Render(ref OutputHandler.SurfaceItem[,] surface)
        {
            for (int y = 0; y < destRect.Height; y++)
            {
                for (int x = 0; x < destRect.Width; x++)
                {

                }
            }
        }
    }
}
