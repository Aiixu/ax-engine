using System;
using System.Diagnostics;

namespace Ax.Engine.Core.Rendering
{
    public abstract class SurfaceRenderer
    {
        public OutputHandler OutputHandler { get; private set; }
        public RenderData LastRenderData { get; private set; }

        public readonly int screenWidth;
        public readonly int screenHeight;

        protected readonly Stopwatch clcStopwatch;
        protected readonly Stopwatch relStopwatch;
        protected readonly Stopwatch wrtStopwatch;
        protected readonly Stopwatch glbStopwatch;

        protected ISurfaceItem[,] surface;
        protected bool[,] surfaceSet;

        protected IntPtr outHandle;

        public SurfaceRenderer(OutputHandler outputHandler, int screenWidth, int screenHeight, bool mesureTime = true)
        {
            OutputHandler = outputHandler;
            OutputHandler.Enable();

            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            surface = new ISurfaceItem[screenWidth, screenHeight];
            surfaceSet = new bool[screenWidth, screenHeight];

            if(mesureTime)
            {
                clcStopwatch = new Stopwatch();
                relStopwatch = new Stopwatch();
                wrtStopwatch = new Stopwatch();
                glbStopwatch = new Stopwatch();
            }
        }

        public abstract bool Render(ISurfaceItem c, int x, int y);

        public virtual void PrepareSurface()
        {
            ResetRecords();
            BeginGlbRecord();

            surface = new ISurfaceItem[screenWidth, screenHeight];
            surfaceSet = new bool[screenWidth, screenHeight];
        }

        public abstract void ReleaseSurface();

        protected void BeginClcRecord() => BeginRecord(clcStopwatch);
        protected void BeginRelRecord() => BeginRecord(relStopwatch);
        protected void BeginWrtRecord() => BeginRecord(wrtStopwatch);
        protected void BeginGlbRecord() => BeginRecord(glbStopwatch);

        protected void EndClcRecord() => EndRecord(clcStopwatch);
        protected void EndRelRecord() => EndRecord(relStopwatch);
        protected void EndWrtRecord() => EndRecord(wrtStopwatch);
        protected void EndGlbRecord() => EndRecord(glbStopwatch);

        protected void ExportRecord()
        {
            LastRenderData = new RenderData()
            {
                CalculationTime = clcStopwatch?.Elapsed ?? TimeSpan.Zero,
                ReleaseTime = relStopwatch?.Elapsed ?? TimeSpan.Zero,
                WriteTime = wrtStopwatch?.Elapsed ?? TimeSpan.Zero,
                GlobalTime = glbStopwatch?.Elapsed ?? TimeSpan.Zero,
                Surface = surface
            };
        }

        private void BeginRecord(Stopwatch stopwatch) => stopwatch?.Start();

        private void EndRecord(Stopwatch stopwatch) => stopwatch?.Stop();

        private void ResetRecords()
        {
            clcStopwatch?.Reset();
            relStopwatch?.Reset();
            wrtStopwatch?.Reset();
            glbStopwatch?.Reset();
        }
    }
}
