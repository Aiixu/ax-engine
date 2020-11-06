﻿using System;

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

            byte[] buffer = new byte[screenWidth * screenHeight * 20];
            int positionInBuffer = 0;

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

                byte[] charBytes = new byte[count];
                Memset(charBytes, 0, count, 32);

                byte[] seqBytes = flattenSurface[i].Bytes ?? RgbSurfaceItem.BaseColorSequence;
                seqBytes[2] = 52;

                Buffer.BlockCopy(seqBytes, 0, buffer, positionInBuffer, 19);
                Buffer.BlockCopy(charBytes, 0, buffer, positionInBuffer + 19, count);

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
            BeginClcRecord();

            surface[x, y] = c;
            surfaceSet[x, y] = true;

            EndClcRecord();

            return true;
        }
    }
}