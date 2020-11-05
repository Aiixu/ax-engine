using System;

namespace Ax.Engine.Core.Rendering
{
    public struct RenderData
    {
        public TimeSpan CalculationTime { get; internal set; }
        public TimeSpan ReleaseTime { get; internal set; }
        public TimeSpan WriteTime { get; internal set; }
        public TimeSpan GlobalTime { get; internal set; }
        public ISurfaceItem[,] Surface { get; internal set; }

        public override string ToString()
        {
            return $"CALC {CalculationTime}, RELE {ReleaseTime}, WRIT {WriteTime}, GLOB {GlobalTime}";
        }
    }
}
