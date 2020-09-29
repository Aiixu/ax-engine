using System;
using System.Text;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Ax.Engine.Utils;

using static Ax.Engine.Core.Native;
using static Ax.Engine.Utils.DefaultValue;

namespace Ax.Engine.Core
{
    public sealed class OutputHandler
    {
        public enum RenderingMode
        {
            VTColorOnlyBackground,
            VTColorOnlyForeground,
            VTColorOnlyBothBackgroundAndForeground,

            VTColorOnlyHalfChar,
            
            VTColorAndChars,
            VTColorOnlyAndChars,

            ASCII
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

        public IntPtr Handle { get => handle; }

        public RenderData LastFrameData { get; private set; }

        private SurfaceItem[,] surface;
        private bool[,] surfaceSet;

        private Encoding consoleEncoding;

        private DateTime lastFrameRendered;
        private int frameDelay;

        private static RenderingMode renderingMode;

        private IntPtr handle;
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
            if (!GetStdOut(out handle)) { return false; }
            if (!GetConsoleModeIn(Handle, out outLast)) { return false; }

            // Output mode
            CONSOLE_MODE_OUTPUT mode = outLast | CONSOLE_MODE_OUTPUT.ENABLE_VIRTUALTERMINALPROCESSING;

            if (disableNewLineAutoReturn) { mode |= CONSOLE_MODE_OUTPUT.DISABLE_NEWLINEAUTORETURN; }

            mode = outLast | CONSOLE_MODE_OUTPUT.ENABLE_VIRTUALTERMINALPROCESSING;

            // Font
            lastFont = new CONSOLE_FONT_INFO_EX();
            GetCurrentConsoleFontEx(handle, false, ref lastFont);

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

            return SetConsoleMode(Handle, (uint)mode);
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
            handle = IntPtr.Zero;

            bool disabled = false;

            disabled &= GetStdOut(out _);
            disabled &= SetConsoleMode(Handle, (uint)outLast);
            
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
                case RenderingMode.VTColorOnlyBackground:
                case RenderingMode.VTColorOnlyForeground:
                case RenderingMode.VTColorOnlyBothBackgroundAndForeground:

                case RenderingMode.VTColorAndChars:
                    surface = new SurfaceItem[width, height];
                    surfaceSet = new bool[width, height];
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
     
        public int ReleaseSurface()
        {
            releaseStopwatch.Start();
            StringBuilder bytesBuilder = new StringBuilder();

            switch (renderingMode)
            {
                case RenderingMode.VTColorOnlyBackground:
                case RenderingMode.VTColorOnlyForeground:
                case RenderingMode.VTColorOnlyBothBackgroundAndForeground:
                    {
                        SurfaceItem[] flattenSurface = surface.To1DArray();
                        List<GroupedSurfaceItem> groupedSurface = new List<GroupedSurfaceItem>();

                        for (int i = 0; i < flattenSurface.Length; i++)
                        {
                            int count = 1;
                            while (i < flattenSurface.Length - 1 && ((flattenSurface[i] == null && flattenSurface[i + 1] == null) || (flattenSurface[i] != null && flattenSurface[i].Equals(flattenSurface[i + 1]))))
                            {
                                i++;
                                count++;
                            }

                            Color surfaceColor = flattenSurface[i]?.color ?? Color.Black;

                            if (renderingMode != RenderingMode.VTColorOnlyBothBackgroundAndForeground)
                            {
                                // VTColorOnlyBackground
                                // VTColorOnlyForeground

                                bytesBuilder.Append(renderingMode == RenderingMode.VTColorOnlyBackground ? GetColorBackgroundString(surfaceColor) : GetColorForegroundString(surfaceColor));
                                bytesBuilder.Append(new string(renderingMode == RenderingMode.VTColorOnlyBackground ? ' ' : '█', count));
                            }
                            else
                            {
                                // VTColorOnlyBothBackgroundAndForeground

                                groupedSurface.Add(new GroupedSurfaceItem(surfaceColor, count));
                            }
                        }

                        if (renderingMode != RenderingMode.VTColorOnlyBothBackgroundAndForeground) { break; }

                        // todo > move to single loop

                        // VTColorOnlyBothBackgroundAndForeground

                        Color lastBackground = null;
                        Color lastForeground = null;

                        const bool LAST_USED_IS_FOREGROUND = true;
                        const bool LAST_USED_IS_BACKGROUND = false;

                        bool lastUsed = false;

                        for (int i = 0; i < groupedSurface.Count; i++)
                        {
                            Color color = groupedSurface[i].color;

                            // 1rst  pass
                            if (lastBackground == null)
                            {
                                bytesBuilder.Append(GetColorBackgroundString(groupedSurface[i].color));
                                bytesBuilder.Append(new string(' ', groupedSurface[i].charCount));

                                lastBackground = groupedSurface[i].color;
                                lastUsed = LAST_USED_IS_BACKGROUND;

                                continue;
                            }

                            if(lastForeground == null)
                            {
                                bytesBuilder.Append(GetColorForegroundString(groupedSurface[i].color));
                                bytesBuilder.Append(new string('█', groupedSurface[i].charCount));

                                lastForeground = groupedSurface[i].color;
                                lastUsed = LAST_USED_IS_FOREGROUND;

                                continue;
                            }


                            // Nth pass
                            if(lastBackground.Equals(color))
                            {
                                bytesBuilder.Append(new string(' ', groupedSurface[i].charCount));
                            }
                            else if (lastForeground.Equals(color))
                            {
                                bytesBuilder.Append(new string('█', groupedSurface[i].charCount));
                            }
                            else
                            {
                                lastBackground = null;
                                lastForeground = null;
                            }

                            

                            continue;

                            if (i >= groupedSurface.Count - 2) { continue; }


                            if (color.Equals(groupedSurface[i + 2].color))
                            {

                            }

                            /*if (i != groupedSurface.Count - 1)
                            {
                                bytesBuilder.Append(GetColorForegroundString(groupedSurface[i + 1].color));
                                bytesBuilder.Append(new string('█', groupedSurface[i + 1].charCount));
                                i++;
                            }*/
                        }
                    }
                    break;

                case RenderingMode.VTColorAndChars:
                    {
                        bool lastPixelIsBackground = true;

                        for (int y = 0; y < surfaceSet.GetLength(1); y++)
                        {
                            for (int x = 0; x < surfaceSet.GetLength(0); x++)
                            {
                                SurfaceItem surfaceItem = surface[x, y];

                                if (surfaceSet[x, y])
                                {
                                    string bg = GetColorBackgroundString(surfaceItem.bg.r, surfaceItem.bg.g, surfaceItem.bg.b);
                                    string fg = GetColorForegroundString(surfaceItem.fg.r, surfaceItem.fg.g, surfaceItem.fg.b);

                                    bytesBuilder.Append(bg);
                                    bytesBuilder.Append(fg);
                                    bytesBuilder.Append(surfaceItem.ch == '\0' ? ' ' : surfaceItem.ch);

                                    lastPixelIsBackground = false;
                                }
                                else
                                {
                                    if (!lastPixelIsBackground)
                                    {
                                        string bg = GetColorBackgroundString(0, 0, 0);
                                        bytesBuilder.Append(bg);

                                        lastPixelIsBackground = true;
                                    }

                                    bytesBuilder.Append(' ');
                                }
                            }
                        }
                    }
                    break;

            }
            releaseStopwatch.Stop();
            
            writeStopwatch.Start();
            Console.SetCursorPosition(0, 0);
            byte[] buffer = consoleEncoding.GetBytes(bytesBuilder.ToString());
            WriteConsole(Handle, buffer, buffer.Length, out int written, IntPtr.Zero);
            writeStopwatch.Stop();

            globalStopwatch.Stop();

            LastFrameData = new RenderData()
            {
                CalculationTime = calculationStopwatch.Elapsed,
                ReleaseTime = releaseStopwatch.Elapsed,
                WriteTime = writeStopwatch.Elapsed,
                GlobalTime = globalStopwatch.Elapsed,
            };

            Logger.Write(renderingMode);
            Logger.Write($"CALC {calculationStopwatch.Elapsed}");
            Logger.Write($"RELE {releaseStopwatch.Elapsed}");
            Logger.Write($"WRIT {writeStopwatch.Elapsed}");
            Logger.Write($"GLOB {globalStopwatch.Elapsed}");
            //Environment.Exit(0);
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
            public Color color = Color.Black;

            // RenderingMode.FullChar
            public Color fg;
            public Color bg;
            public char ch;

            // Global
            public int z;

            public bool Equals(SurfaceItem other) => renderingMode switch
            {
                // REDUCE WITH C# 9.0 OR
                RenderingMode.VTColorOnlyBackground => other != null && color.Equals(other.color),
                RenderingMode.VTColorOnlyForeground => other != null && color.Equals(other.color),
                RenderingMode.VTColorOnlyBothBackgroundAndForeground => other != null && color.Equals(other.color),
                RenderingMode.VTColorAndChars => other != null && ch == other.ch && fg.Equals(other.fg) && bg.Equals(other.bg),
                _ => throw new Exception("Unreachable"),
            };
        }

        private struct GroupedSurfaceItem
        {
            public Color color;
            public int charCount;

            public GroupedSurfaceItem(Color color, int charCount)
            {
                this.color = color;
                this.charCount = charCount;
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
                