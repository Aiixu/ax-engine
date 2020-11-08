using System;
using System.Collections.Generic;

using static Ax.Engine.Core.MemoryHelper;

namespace Ax.Engine.Core.Rendering
{
    internal sealed class QueuedSurfaceRenderer : SurfaceRenderer
    {
        public QueuedSurfaceRenderer(OutputHandler outputHandler, int screenWidth, int screenHeight, bool mesureTime = true)
            : base(outputHandler, screenWidth, screenHeight, mesureTime)
        { }

        private readonly Queue<byte[]> BufferQueue = new Queue<byte[]>();

        public override void ReleaseSurface()
        {
            BeginRelRecord();

            Console.SetCursorPosition(0, 0);

            RgbSurfaceItem[] flattenSurface = new RgbSurfaceItem[screenWidth * screenHeight];
            bool[] flattenSurfaceSet = new bool[screenWidth * screenHeight];
            int index = 0;

            for (int y = 0; y < screenHeight; y++)
            {
                for (int x = 0; x < screenWidth; x++)
                {
                    flattenSurface[index] = surface[x, y] == null ? default : (RgbSurfaceItem)surface[x, y];
                    flattenSurfaceSet[index++] = surfaceSet[x, y];
                }
            }

            for (int i = 0; i < flattenSurface.Length; i++)
            {
                int count = 1;
                while (
                    i < flattenSurface.Length - 1 && 
                    ((!flattenSurfaceSet[i] && !flattenSurfaceSet[i + 1]) || 
                    (flattenSurfaceSet[i] && flattenSurface[i].Equals(flattenSurface[i + 1]))))
                {
                    i++;
                    count++;
                }

                int bufferLength = 19 + count;

                byte[] buffer = new byte[bufferLength];
                byte[] seqBytes = flattenSurface[i].Bytes ?? RgbSurfaceItem.BaseColorSequence;
                seqBytes[2] = 52;

                Buffer.BlockCopy(seqBytes, 0, buffer, 0, 19);
                //Memset(buffer, 19, count, 32);

                BufferQueue.Enqueue(buffer);
            }

            EndRelRecord();

            OutputHandler.EndWrite();

            EndGlbRecord();
            ExportRecord();
        }

        public override bool Render(ISurfaceItem c, int x, int y)
        {
            BeginClcRecord();

            surface[x, y] = c;
            surfaceSet[x, y] = true;

            EndClcRecord();

            return true;
        }
    }
}
