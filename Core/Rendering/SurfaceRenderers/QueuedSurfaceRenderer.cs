using System;

using static Ax.Engine.Core.Memory;

namespace Ax.Engine.Core.Rendering
{
    internal sealed class QueuedSurfaceRenderer : SurfaceRenderer
    {
        public QueuedSurfaceRenderer(OutputHandler outputHandler, int screenWidth, int screenHeight)
            : base(outputHandler, screenWidth, screenHeight)
        { }
        
        public override void ReleaseSurface()
        {
            BeginRelRecord();

            byte[] buffer = new byte[screenWidth * screenHeight * 20];
            int positionInBuffer = 0;

            RgbSurfaceItem[] flattenSurface = new RgbSurfaceItem[screenWidth * screenHeight];
            bool[] flattenSurfaceSet = new bool[screenWidth * screenHeight];
            int index = 0;

            for (int x = 0; x < screenHeight; x++)
            {
                for (int y = 0; y < screenWidth; y++)
                {
                    flattenSurface[index] = (RgbSurfaceItem)surface[x, y];
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

                byte[] charBytes = new byte[count];
                Memset(charBytes, 0, count, 32);

                Buffer.BlockCopy(flattenSurface[i].Bytes, 0, buffer, positionInBuffer, 19);
                Buffer.BlockCopy(charBytes, 0, buffer, positionInBuffer + 19, count);

                positionInBuffer += 19 + count;
            }

            EndRelRecord();

            BeginWrtRecord();

            Console.SetCursorPosition(0, 0);
            outputHandler.Write(buffer, positionInBuffer);
            outputHandler.EndWrite();

            EndWrtRecord();

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
