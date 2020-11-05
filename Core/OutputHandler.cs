namespace Ax.Engine.Core
{
    public sealed class OutputHandler
    {

        /*
        internal bool RenderCh(int x, int y, int z, char ch, byte fgr, byte fgg, byte fgb, byte bgr, byte bgg, byte bgb, bool forced = false)
        {
            return RenderCh(x, y, z, ch, new Color(fgr, fgg, fgb), new Color(bgr, bgg, bgb), forced);
        }

        internal bool RenderCh(int x, int y, int z, char ch, Color fg, Color bg = null, bool forced = false)
        {
            bool ProcessRenderInternal()
            {
                if (x < 0 || x >= surfaceSet.GetLength(0) || y < 0 || y >= surfaceSet.GetLength(1)) { return false; }

                // z == int.MaxValue > dev tools
                if (forced || !surfaceSet[x, y] || (surfaceSet[x, y] && surface[x, y].z >= z && surface[x, y].z != int.MaxValue))
                {
                    SurfaceItem surfaceItem = new SurfaceItem()
                    {
                        z = z
                    };

                    switch (renderingMode)
                    {
                        case RenderingMode.VTColorOnlyBackground:
                        case RenderingMode.VTColorOnlyForeground:
                        case RenderingMode.VTColorOnlyBothBackgroundAndForeground:
                            surfaceItem.color = bg;
                            break;

                        case RenderingMode.VTColorAndChars:
                            surfaceItem.ch = ch;
                            surfaceItem.fg = fg;
                            surfaceItem.bg = bg;
                            break;
                    }

                    if (surfaceSet[x, y] && surface[x, y].Equals(surfaceItem)) { return false; }

                    surface[x, y] = surfaceItem;
                    surfaceSet[x, y] = true;

                    return true;
                }

                return false;
            }

            calculationStopwatch.Start();
            bool result = ProcessRenderInternal();
            calculationStopwatch.Stop();

            return result;
        }

        internal bool[] RenderStr(int x, int y, int z, string str, byte fgr, byte fgg, byte fgb, byte bgr, byte bgg, byte bgb, bool forced = false) => RenderStr(x, y, z, str, new Color(fgr, fgg, fgb), new Color(bgr, bgg, bgb), forced);
        internal bool[] RenderStr(int x, int y, int z, string str, Color fg, Color bg = null, bool forced = false)
        {
            bool[] results = new bool[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                results[i] = RenderCh(x + i, y, z, str[i], fg, bg, forced);
            }

            return results;
        }
        */

    }
}
