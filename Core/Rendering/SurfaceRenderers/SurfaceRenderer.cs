using System;

namespace Ax.Engine.Core.Rendering
{
    public abstract class SurfaceRenderer
    {
        public readonly int screenWidth;
        public readonly int screenHeight;

        protected OutputHandler outputHandler;

        protected ISurfaceItem[,] surface;
        protected bool[,] surfaceSet;

        protected IntPtr outHandle;

        public SurfaceRenderer(OutputHandler outputHandler, int screenWidth, int screenHeight)
        {
            outputHandler.Enable();

            this.outputHandler = outputHandler;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            surface = new ISurfaceItem[screenWidth, screenHeight];
            surfaceSet = new bool[screenWidth, screenHeight];
        }

        public abstract bool Render(ISurfaceItem c, int x, int y);

        public virtual void PrepareSurface()
        {
            surface = new ISurfaceItem[screenWidth, screenHeight];
            surfaceSet = new bool[screenWidth, screenHeight];
        }

        public abstract void ReleaseSurface();
    }
}
