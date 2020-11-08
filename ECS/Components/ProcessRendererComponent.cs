using System;
using System.Drawing;
using Ax.Engine.Utils;

using Ax.Engine.Core;
using Ax.Engine.Core.Rendering;
using System.Diagnostics;

namespace Ax.Engine.ECS.Components
{
    // TODO -> remove
    public sealed class ProcessRendererComponent : Component
    {
        private IntPtr proc;
        private RectInt destRect;

        private Bitmap frame;

        public override void Init()
        {
            destRect = new RectInt(0, 0, 0, 0);
        }

        public override void Update()
        {
            frame = new Bitmap(Imaging.CaptureScreen(), 120, 45);
        }

        public override void Render(SurfaceRenderer renderer)
        {
            for (int y = 0; y < frame.Height; y++)
            {
                for (int x = 0; x < frame.Width; x++)
                {
                    Color pixel = frame.GetPixel(x, y);
                    if (pixel.A == 0) { continue; } // todo: implement alpha threshold

                    renderer.Render(new RgbSurfaceItem(pixel), destRect.X + x, destRect.Y + y);
                }
            }

            frame.Dispose();
        }

        public void AttachProcess(IntPtr proc)
        {
            this.proc = proc;
        }
    }
}
