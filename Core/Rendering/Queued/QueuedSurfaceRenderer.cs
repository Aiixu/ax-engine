using System;
using System.Diagnostics;

namespace Ax.Engine.Core.Rendering
{
    internal sealed class QueuedSurfaceRenderer : SurfaceRenderer
    {
        public QueuedSurfaceRenderer(IntPtr outHandle, int screenWidth, int screenHeight)
            : base(outHandle, screenWidth, screenHeight)
        { }
        
        public override void PrepareSurface()
        {

        }

        Stopwatch stopwatch = new Stopwatch();
        public long total = 0;
        public override void ReleaseSurface()
        {

        }

        public override bool Render(Color c, int x, int y)
        {
            surface[x, y] = c;
            surfaceSet[x, y] = true;

            return true;
        }
    }
}
