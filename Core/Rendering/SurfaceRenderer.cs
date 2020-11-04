using System;

namespace Ax.Engine.Core.Rendering
{
    public abstract class SurfaceRenderer
    {
        public readonly int screenWidth;
        public readonly int screenHeight;

        protected Color[,] surface;
        protected bool[,] surfaceSet;

        protected IntPtr outHandle;

        public SurfaceRenderer(IntPtr outHandle, int screenWidth, int screenHeight)
        {
            this.outHandle = outHandle;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            surface = new Color[screenWidth, screenHeight];
            surfaceSet = new bool[screenWidth, screenHeight];
        }

        public virtual bool Render(Color c, int x, int y) => false;

        public abstract void PrepareSurface();

        public abstract void ReleaseSurface();
    }
}
