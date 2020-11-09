using System;
using System.Xml.Linq;
using static Ax.Engine.Core.MemoryHelper;
using static Ax.Engine.Core.Native.WinApi;

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

            int x = 0;
            int y = 0;

            int nx = 1;
            int ny = 0;

            for (int i = 0; i < surfaceSize; i++)
            {
                int count = 1;

                // count contiguous surface items
                while (
                    i < surfaceSize - 1 &&
                    ((!surfaceSet[x, y] && !surfaceSet[nx, ny]) ||
                    (surfaceSet[x, y] && surface[x, y].Equals(surface[nx, ny]))))
                {
                    i++;
                    count++;

                    x++;
                    if (x >= screenWidth)
                    {
                        x = 0;
                        y++;

                        nx = 1;
                        ny++;
                    }
                    else
                    {
                        nx = x + 1;
                        if (nx >= screenWidth)
                        {
                            nx = 0;
                            ny++;
                        }
                    }
                }

                byte[] surfaceItemSeq = surface[x, y].Bytes ?? RgbSurfaceItem.BaseColorSequence;
                // set to background
                surfaceItemSeq[2] = 52;

                // block copy color sequence
                Buffer.BlockCopy(surfaceItemSeq, 0, buffer, positionInBuffer, 19);
                // set chars in buffer
                Memset(buffer, positionInBuffer + 19, count, 32); // 32 -> white space

                positionInBuffer += 19 + count;

                x++;
                if (x >= screenWidth)
                {
                    x = 0;
                    y++;
                }
                else
                {
                    nx = x + 1;
                    if (nx >= screenWidth)
                    {
                        nx = 0;
                        ny++;
                    }
                }
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
            if (x < 0 || x >= screenWidth || y < 0 || y >= screenHeight) { return false; }

            BeginClcRecord();

            surface[x, y] = c;
            surfaceSet[x, y] = true;

            EndClcRecord();

            return true;
        }
    }
}
