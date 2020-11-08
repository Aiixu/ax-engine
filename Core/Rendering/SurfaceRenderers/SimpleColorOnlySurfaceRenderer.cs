using System;

using static Ax.Engine.Core.MemoryHelper;

namespace Ax.Engine.Core.Rendering
{
    internal sealed class SimpleColorOnlySurfaceRenderer : SurfaceRenderer
    {
        public SimpleColorOnlySurfaceRenderer(OutputHandler outputHandler, int screenWidth, int screenHeight, bool mesureTime = true)
            : base(outputHandler, screenWidth, screenHeight, mesureTime)
        { }

        public override void ReleaseSurface()
        {
            BeginRelRecord();

            int surfaceSize = screenWidth * screenHeight;

            byte[] buffer = new byte[surfaceSize * 20];
            int positionInBuffer = 0;

            RgbSurfaceItem[] flattenSurface = new RgbSurfaceItem[surfaceSize];
            bool[] flattenSurfaceSet = new bool[surfaceSize];
            int index = 0;

            for (int y = 0; y < screenHeight; y++)
            {
                for (int x = 0; x < screenWidth; x++)
                {
                    flattenSurface[index] = surface[x, y] == null ? default : (RgbSurfaceItem)surface[x, y];
                    flattenSurfaceSet[index] = surfaceSet[x, y];

                    index++;
                }
            }

            for (int i = 0; i < surfaceSize; i++)
            {
                int count = 1;

                // count contiguous surface items
                while(
                    i < surfaceSize - 1 &&
                    ((!flattenSurfaceSet[i] && !flattenSurfaceSet[i + 1]) ||
                    (flattenSurfaceSet[i] && flattenSurface[i].Equals(flattenSurface[i + 1]))))
                {
                    i++;
                    count++;
                }

                byte[] surfaceItemSeq = flattenSurface[i].Bytes ?? RgbSurfaceItem.BaseColorSequence;
                // set to background
                surfaceItemSeq[2] = 52;

                // block copy color sequence
                Buffer.BlockCopy(surfaceItemSeq, 0, buffer, positionInBuffer, 19);
                // set chars in buffer
                Memset(buffer, positionInBuffer + 19, count, 32); // 32 -> white space

                positionInBuffer += 19 + count;
            }

            EndRelRecord();
            BeginWrtRecord();

            Console.SetCursorPosition(0, 0);
            OutputHandler.Write(buffer, positionInBuffer);
            OutputHandler.EndWrite();

            EndWrtRecord();

            EndGlbRecord();
            ExportRecord();
        }

        public override bool Render(ISurfaceItem c, int x, int y)
        {
            if(x < 0 || x >= screenWidth || y < 0 || y >= screenHeight) { return false; }

            BeginClcRecord();

            surface[x, y] = c;
            surfaceSet[x, y] = true;

            EndClcRecord();

            return true;
        }
    }
}
