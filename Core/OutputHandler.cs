using System;
using System.Text;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Ax.Engine.Utils;

using static Ax.Engine.Core.Native;
using static Ax.Engine.Utils.DefaultValue;
using System.Net.NetworkInformation;
using System.Collections.Generic;

namespace Ax.Engine.Core
{
    public sealed class OutputHandler
    {
        public enum RenderingMode
        {
            ColorOnly,
            ColorOnlyHalfChar,
            FullChar,
        }

        public const string ESC = "\x1b";
        public const string BEL = "\x07";
        public const string SUB = "\x1a";
        public const string DEL = "\x7f";

        public const string BL_BRIGHTNESS_TABLE = "█▓▒░ ";

        public static string GetColorBackgroundString(Color c) => GetColorBackgroundString(c.r, c.g, c.b);
        public static string GetColorForegroundString(Color c) => GetColorForegroundString(c.r, c.g, c.b);

        public static string GetColorBackgroundString(byte r, byte g, byte b) => string.Concat(ESC, "[48;2;", BytesMap[r], ";", BytesMap[g], ";", BytesMap[b], "m");
        public static string GetColorForegroundString(byte r, byte g, byte b) => string.Concat(ESC, "[38;2;", BytesMap[r], ";", BytesMap[g], ";", BytesMap[b], "m");

        private static readonly string[] BytesMap = Enumerable.Range(0, 256).Select(s => s.ToString()).ToArray();

        public IntPtr HOUT { get => hOut; }

        public RenderData LastFrameData { get; private set; }

        private SurfaceItem[,] surface;
        private bool[,] surfaceSet;

        private Encoding consoleEncoding;

        private DateTime lastFrameRendered;
        private int frameDelay;

        private static RenderingMode renderingMode;

        private IntPtr hOut;
        private CONSOLE_MODE_OUTPUT outLast;

        internal CONSOLE_FONT_INFO_EX lastFont;

        private Stopwatch calculationStopwatch;
        private Stopwatch releaseStopwatch;
        private Stopwatch writeStopwatch;
        private Stopwatch globalStopwatch;

        public static Color ClearColor = Color.Black;

        // TODO : fontHeight
        public bool Enable(RenderingMode renderingMode, string fontName, int fontWidth, int fontHeight, bool cursorVisible, bool disableNewLineAutoReturn, int frameDelay)
        {
            if (!GetStdOut(out hOut)) { return false; }
            if (!GetConsoleModeIn(HOUT, out outLast)) { return false; }

            // Output mode
            CONSOLE_MODE_OUTPUT mode = outLast | CONSOLE_MODE_OUTPUT.ENABLE_VIRTUALTERMINALPROCESSING;

            if (disableNewLineAutoReturn) { mode |= CONSOLE_MODE_OUTPUT.DISABLE_NEWLINEAUTORETURN; }

            mode = outLast | CONSOLE_MODE_OUTPUT.ENABLE_VIRTUALTERMINALPROCESSING;

            // Font
            lastFont = new CONSOLE_FONT_INFO_EX();
            GetCurrentConsoleFontEx(hOut, false, ref lastFont);

            Default(ref fontName, StringNotNullOrEmpty, lastFont.FaceName);
            Default(ref fontWidth, IntegerPositive, lastFont.dwFontSize.X);
            Default(ref fontHeight, IntegerPositive, lastFont.dwFontSize.Y);

            CONSOLE_FONT_INFO_EX newFont = new CONSOLE_FONT_INFO_EX();

            newFont.cbSize = (uint)Marshal.SizeOf(newFont);
            newFont.FaceName = fontName;
            newFont.dwFontSize.X = (short)fontWidth;
            newFont.dwFontSize.Y = (short)fontHeight;

            SetCurrentConsoleFontEx(GetStdHandle(STD_OUTPUT_HANDLE), false, ref newFont);
            
            calculationStopwatch = new Stopwatch();
            releaseStopwatch = new Stopwatch();
            writeStopwatch = new Stopwatch();
            globalStopwatch = new Stopwatch();

            this.frameDelay = frameDelay;

            consoleEncoding = Console.Out.Encoding;

            OutputHandler.renderingMode = renderingMode;
            Console.CursorVisible = cursorVisible;

            return SetConsoleMode(HOUT, (uint)mode);
        }

        public void WaitFrame()
        {
            if ((DateTime.Now - lastFrameRendered).TotalMilliseconds >= frameDelay)
            {
                lastFrameRendered = DateTime.Now;
            }
            else
            {
                Thread.Sleep((int)(frameDelay - (DateTime.Now - lastFrameRendered).TotalMilliseconds));
            }
        }

        public bool Disable()
        {
            hOut = IntPtr.Zero;

            bool disabled = false;

            disabled &= GetStdOut(out _);
            disabled &= SetConsoleMode(HOUT, (uint)outLast);
            
            SetCurrentConsoleFontEx(GetStdHandle(STD_OUTPUT_HANDLE), false, ref lastFont);

            return disabled;
        }

        public void PrepareSurface(int width, int height)
        {
            // canPrepareSurface = false;

            calculationStopwatch.Reset();
            releaseStopwatch.Reset();
            writeStopwatch.Reset();
            globalStopwatch.Reset();

            globalStopwatch.Start();

            switch(renderingMode)
            {
                case RenderingMode.ColorOnly:
                case RenderingMode.FullChar:
                    surface = new SurfaceItem[width, height];
                    surfaceSet = new bool[width, height];
                    break;

                case RenderingMode.ColorOnlyHalfChar:
                    surface = new SurfaceItem[width, MathHelper.CeilToInt(height / 2f)];
                    surfaceSet = new bool[width, MathHelper.CeilToInt(height / 2f)];
                    break;
            }
        }

        internal bool RenderCh(int x, int y, int z, char ch, byte fgr, byte fgg, byte fgb, byte bgr, byte bgg, byte bgb, bool forced = false)
        {
            return RenderCh(x, y, z, ch, new Color(fgr, fgg, fgb), new Color(bgr, bgg, bgb), forced);
        }

        internal bool RenderCh(int x, int y, int z, char ch, Color fg, Color bg = null, bool forced = false)
        {
            bool ProcessRenderInternal()
            {
                // z == int.MaxValue > dev tools
                if (forced || !surfaceSet[x, y] || (surfaceSet[x, y] && surface[x, y].z >= z && surface[x, y].z != int.MaxValue))
                {
                    SurfaceItem surfaceItem = new SurfaceItem()
                    {
                        z = z
                    };

                    switch(renderingMode)
                    {
                        case RenderingMode.ColorOnly:
                        case RenderingMode.ColorOnlyHalfChar:
                            surfaceItem.color = bg;
                            break;

                        case RenderingMode.FullChar:
                            surfaceItem.bg = bg;
                            surfaceItem.fg = fg;
                            surfaceItem.ch = ch;
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
     
        public int ReleaseSurface()
        {
            releaseStopwatch.Start();
            StringBuilder bytesBuilder = new StringBuilder();

            /*int size = surface.Length;
            SurfaceItem[] flattenSurface = new SurfaceItem[size];
            List<SurfaceItem> optimizedSurface = new List<SurfaceItem>(size);

            int write = 0;
            for (int x = 0; x <= surface.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= surface.GetUpperBound(1); y++)
                {
                    flattenSurface[write++] = surface[x, y];
                }
            }*/

            switch(renderingMode)
            {
                case RenderingMode.ColorOnly:
                    {
                        int width = surfaceSet.GetLength(0);

                        Color[] flattenSurface =  surface.To1DArray(item => item?.color ?? Color.Black);

                        List<(Color, int count)> groupedSurface = new List<(Color, int count)>();
                        for (int i = 0; i < flattenSurface.Length; i++)
                        {
                            int count = 1;
                            while (i < flattenSurface.Length - 1 && flattenSurface[i].Equals(flattenSurface[i + 1]))
                            {
                                i++;
                                count++;
                            }

                            groupedSurface.Add((flattenSurface[i], count));
                        }

                        
                        
                        /*

                        bool lastPixelIsBackground = true;
                        List<string> builder = new List<string>();

                        for (int y = 0; y < surfaceSet.GetLength(1); y++)
                        {
                            for (int x = 0; x < surfaceSet.GetLength(0); x++)
                            {
                                SurfaceItem surfaceItem = surface[x, y];

                                if(lastBackground == surfaceItem.color)
                                {
                                    builder[builder.Count - 1] += ' ';
                                    continue;
                                }
                                else if(lastForeground == surfaceItem.color)
                                {
                                    builder[builder.Count - 1] += '█';
                                    continue;
                                }

                                string bg = GetColorBackgroundString(surfaceItem.bg.r, surfaceItem.bg.g, surfaceItem.bg.b);
                                string fg = GetColorForegroundString(surfaceItem.fg.r, surfaceItem.fg.g, surfaceItem.fg.b);

                                bytesBuilder.Append(bg);
                                bytesBuilder.Append(fg);
                                bytesBuilder.Append(surfaceItem.ch == '\0' ? ' ' : surfaceItem.ch);

                                lastPixelIsBackground = false;
                                continue;
                                if (surfaceSet[x, y])
                                {
                                    
                                }
                                else
                                {
                                    if (!lastPixelIsBackground)
                                    {
                                        //string bg = GetColorBackgroundString(0, 0, 0);
                                        //bytesBuilder.Append(bg);

                                        lastPixelIsBackground = true;
                                    }

                                    bytesBuilder.Append(' ');
                                }
                            }
                        }*/
                    }
                    break;
            }
            releaseStopwatch.Stop();
            
            writeStopwatch.Start();
            Console.SetCursorPosition(0, 0);
            byte[] buffer = consoleEncoding.GetBytes(bytesBuilder.ToString());
            WriteConsole(HOUT, buffer, buffer.Length, out int written, IntPtr.Zero);
            writeStopwatch.Stop();

            globalStopwatch.Stop();

            LastFrameData = new RenderData()
            {
                CalculationTime = calculationStopwatch.Elapsed,
                ReleaseTime = releaseStopwatch.Elapsed,
                WriteTime = writeStopwatch.Elapsed,
                GlobalTime = globalStopwatch.Elapsed,
            };

            // canPrepareSurface = true;

            return 0; //written
        }

        private bool GetStdOut(out IntPtr handle)
        {
            handle = GetStdHandle(STD_OUTPUT_HANDLE);
            return handle != INVALID_HANDLE;
        }

        private bool GetConsoleModeIn(IntPtr hConsoleHandle, out CONSOLE_MODE_OUTPUT mode)
        {
            if (!GetConsoleMode(hConsoleHandle, out uint lpMode))
            {
                mode = 0;
                return false;
            }

            mode = (CONSOLE_MODE_OUTPUT)lpMode;
            return true;
        }

        private class SurfaceItem : IEquatable<SurfaceItem>
        {
            // RenderingMode.ColorOnly
            public Color color;

            // RenderingMode.FullChar
            public Color fg;
            public Color bg;
            public char ch;

            // Global
            public int z;

            public bool Equals(SurfaceItem other)
            {
                switch(renderingMode)
                {
                    case RenderingMode.ColorOnly:
                    case RenderingMode.ColorOnlyHalfChar:
                        return color.Equals(other.color);

                    case RenderingMode.FullChar:
                        return fg.Equals(other.fg) && bg.Equals(other.bg) && ch == other.ch && z == other.z;

                    default:
                        throw new Exception("Unreachable");
                }
            }
        }
        
        public struct RenderData
        {
            public TimeSpan CalculationTime { get; internal set; } 
            public TimeSpan ReleaseTime { get; internal set; }
            public TimeSpan WriteTime { get; internal set; }
            public TimeSpan GlobalTime { get; internal set; }
        }
    }
}
                