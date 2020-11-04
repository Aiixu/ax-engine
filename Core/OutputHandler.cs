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

using Color = Ax.Engine.Utils.Color;
using System.Reflection.Emit;
using System.Reflection;

namespace Ax.Engine.Core
{
    public sealed class OutputHandler
    {
        private delegate void MemorySetter(IntPtr array, byte value, int count);
        private MemorySetter MemsetDelegate;

        public enum RenderingMode : byte
        {
            MultiThreaded = 0,

            VTColorOnlyForeground = 51,
            VTColorOnlyBackground = 52,
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

        internal CONSOLE_FONT_INFOEX lastFont;

        private Stopwatch calculationStopwatch;
        private Stopwatch releaseStopwatch;
        private Stopwatch writeStopwatch;
        private Stopwatch globalStopwatch;

        public static Color ClearColor = Color.Black;

        // TODO : fontHeight
        public bool Enable(ref StringBuilder logger, RenderingMode renderingMode, string fontName, int fontWidth, int fontHeight, bool cursorVisible, bool disableNewLineAutoReturn, int frameDelay)
        {
            bool stdOut;
            logger.AppendLine($"GETSTDOUT      {stdOut = GetStdOut(out handle)}");

            if (!stdOut) { return false; }
            if (!GetConsoleModeOut(Handle, ref logger, out outLast)) { return false; }

            // Output mode
            CONSOLE_MODE_OUTPUT mode = outLast | CONSOLE_MODE_OUTPUT.ENABLE_VIRTUAL_TERMINAL_PROCESSING;

            if (disableNewLineAutoReturn) { mode |= CONSOLE_MODE_OUTPUT.DISABLE_NEWLINE_AUTO_RETURN; }

            mode = outLast | CONSOLE_MODE_OUTPUT.ENABLE_VIRTUAL_TERMINAL_PROCESSING;

            logger.AppendLine($"OUTLAST        {outLast}");
            logger.AppendLine($"OUTMODE        {mode}");

            // Font
            lastFont = new CONSOLE_FONT_INFOEX();
            logger.AppendLine($"GETFONT        {GetCurrentConsoleFontEx(handle, false, ref lastFont)}");

            Default(ref fontName, StringNotNullOrEmpty, lastFont.FaceName);
            Default(ref fontWidth, IntegerPositive, lastFont.dwFontSize.X);
            Default(ref fontHeight, IntegerPositive, lastFont.dwFontSize.Y);

            CONSOLE_FONT_INFOEX newFont = new CONSOLE_FONT_INFOEX();

            newFont.cbSize = (uint)Marshal.SizeOf(newFont);
            newFont.FaceName = fontName;
            newFont.dwFontSize.X = (short)fontWidth;
            newFont.dwFontSize.Y = (short)fontHeight;

            logger.AppendLine($"SETFONT        {SetCurrentConsoleFontEx(GetStdHandle((uint)HANDLE.STD_OUTPUT_HANDLE), false, ref newFont)}");

            calculationStopwatch = new Stopwatch();
            releaseStopwatch = new Stopwatch();
            writeStopwatch = new Stopwatch();
            globalStopwatch = new Stopwatch();

            this.frameDelay = frameDelay;

            consoleEncoding = Console.Out.Encoding;

            OutputHandler.renderingMode = renderingMode;
            Console.CursorVisible = cursorVisible;

            bool cMode;
            logger.AppendLine($"SETCMODEOUT    {cMode = SetConsoleMode(Handle, (uint)mode)}");

            DynamicMethod m = new DynamicMethod("memset", MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, typeof(void), new[] { typeof(IntPtr), typeof(byte), typeof(int) }, typeof(OutputHandler), false);
            
            ILGenerator il = m.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0); // address
            il.Emit(OpCodes.Ldarg_1); // initialization value
            il.Emit(OpCodes.Ldarg_2); // number of bytes
            il.Emit(OpCodes.Initblk);
            il.Emit(OpCodes.Ret);

            MemsetDelegate = (MemorySetter)m.CreateDelegate(typeof(MemorySetter));

            return cMode;
        }

        public void WaitFrame()
        {
            if ((DateTime.Now - lastFrameRendered).TotalMilliseconds >= frameDelay)
            {
                lastFrameRendered = DateTime.Now;
            }

            Thread.Sleep((int)(frameDelay - (DateTime.Now - lastFrameRendered).TotalMilliseconds));
        }

        public bool Disable()
        {
            handle = IntPtr.Zero;

            bool disabled = false;

            disabled &= GetStdOut(out _);
            disabled &= SetConsoleMode(Handle, (uint)outLast);

            SetCurrentConsoleFontEx(GetStdHandle((uint)HANDLE.STD_OUTPUT_HANDLE), false, ref lastFont);

            return disabled;
        }

        private byte[] surfaceBuffer;
        public void PrepareSurface(int width, int height)
        {
            // canPrepareSurface = false;

            calculationStopwatch.Reset();
            releaseStopwatch.Reset();
            writeStopwatch.Reset();
            globalStopwatch.Reset();

            globalStopwatch.Start();

            //surfaceBuffer = new byte[width * height * 20];

            switch (renderingMode)
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

        public int ReleaseSurface()
        {
            releaseStopwatch.Start();
            StringBuilder bytesBuilder = new StringBuilder();

            Color background = new Color(0, 0, 0);

            int byteOffset = 0;
            byte[] byteBuffer = new byte[surface.GetLength(0) * surface.GetLength(1) * 20];

            switch (renderingMode)
            {
                case RenderingMode.VTColorOnlyBackground:
                case RenderingMode.VTColorOnlyForeground:
                case RenderingMode.VTColorOnlyBothBackgroundAndForeground:
                    {
                        byte[] baseColorSequence = new byte[]
                        {
                            27, 91, 0, 56, 59, 50, 59, 0, 0, 0, 59, 0, 0, 0, 59, 0, 0, 0, 109
                        };

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
                                
                                baseColorSequence[2] = (byte)renderingMode;

                                // red
                                baseColorSequence[7] = (byte)(surfaceColor.r / 100 % 10 + 48);
                                baseColorSequence[8] = (byte)(surfaceColor.r / 010 % 10 + 48);
                                baseColorSequence[9] = (byte)(surfaceColor.r / 001 % 10 + 48);

                                // green
                                baseColorSequence[11] = (byte)(surfaceColor.g / 100 % 10 + 48);
                                baseColorSequence[12] = (byte)(surfaceColor.g / 010 % 10 + 48);
                                baseColorSequence[13] = (byte)(surfaceColor.g / 001 % 10 + 48);

                                // blue              
                                baseColorSequence[15] = (byte)(surfaceColor.b / 100 % 10 + 48);
                                baseColorSequence[16] = (byte)(surfaceColor.b / 010 % 10 + 48);
                                baseColorSequence[17] = (byte)(surfaceColor.b / 001 % 10 + 48);

                                byte[] charBytes = new byte[count];
                                Memset(charBytes, 0, count, 32);

                                Buffer.BlockCopy(baseColorSequence, 0, byteBuffer, byteOffset, 19);
                                Buffer.BlockCopy(charBytes, 0, byteBuffer, byteOffset + 19, count);

                                byteOffset += 19 + count;

                                continue;
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

                        // implement VTColorOnlyBothBackgroundAndForeground
                    }
                    break;

                case RenderingMode.VTColorAndChars:
                    {
                        bool lastPixelIsBackground = true;

                        int height = surfaceSet.GetLength(1);
                        int width = surfaceSet.GetLength(0);

                        Dictionary<Color, string> storedValues = new Dictionary<Color, string>()
                        {
                            { background, GetColorBackgroundString(background) }
                        };

                        for (int y = 0; y < height; y++)
                        {
                            for (int x = 0; x < width; x++)
                            {
                                SurfaceItem surfaceItem = surface[x, y];

                                if (surfaceSet[x, y])
                                {
                                    if (surfaceItem.ch != '\0')
                                    {
                                        if (!storedValues.ContainsKey(surfaceItem.fg))
                                        {
                                            storedValues.Add(surfaceItem.fg, GetColorForegroundString(surfaceItem.fg.r, surfaceItem.fg.g, surfaceItem.fg.b));
                                        }

                                        bytesBuilder.Append(storedValues[surfaceItem.fg]);
                                    }

                                    if (!storedValues.ContainsKey(surfaceItem.bg))
                                    {
                                        storedValues.Add(surfaceItem.bg, GetColorBackgroundString(surfaceItem.bg.r, surfaceItem.bg.g, surfaceItem.bg.b));
                                    }

                                    bytesBuilder.Append(storedValues[surfaceItem.bg]);
                                    bytesBuilder.Append(surfaceItem.ch == '\0' ? ' ' : surfaceItem.ch);

                                    lastPixelIsBackground = false;
                                }
                                else
                                {
                                    if (!lastPixelIsBackground)
                                    {
                                        bytesBuilder.Append(storedValues[background]);

                                        lastPixelIsBackground = true;
                                    }

                                    bytesBuilder.Append(' ');
                                }
                            }
                        }
                    }
                    break;

                case RenderingMode.VTColorOnlyHalfChar:
                    {

                    }
                    break;
            }
            releaseStopwatch.Stop();

            // WRIT
            writeStopwatch.Start();

            Console.SetCursorPosition(0, 0);
            //byte[] buffer = consoleEncoding.GetBytes(bytesBuilder.ToString());
            WriteConsole(Handle, byteBuffer, byteOffset, out int written, IntPtr.Zero);

            writeStopwatch.Stop();

            globalStopwatch.Stop();

            LastFrameData = new RenderData()
            {
                CalculationTime = calculationStopwatch.Elapsed,
                ReleaseTime = releaseStopwatch.Elapsed,
                WriteTime = writeStopwatch.Elapsed,
                GlobalTime = globalStopwatch.Elapsed,
                Surface = surface
            };

            /*Logger.Write(renderingMode);
            Logger.Write($"CALC {calculationStopwatch.Elapsed}");
            Logger.Write($"RELE {releaseStopwatch.Elapsed}");
            Logger.Write($"WRIT {writeStopwatch.Elapsed}");
            Logger.Write($"GLOB {globalStopwatch.Elapsed}");*/
            //Environment.Exit(0);
            // canPrepareSurface = true;

            return written; //written
        }

        private bool GetStdOut(out IntPtr handle)
        {
            handle = GetStdHandle((uint)HANDLE.STD_OUTPUT_HANDLE);
            return handle != INVALID_HANDLE;
        }

        private bool GetConsoleModeOut(IntPtr hConsoleHandle, ref StringBuilder logger, out CONSOLE_MODE_OUTPUT mode)
        {
            bool cMode;
            logger.AppendLine($"GETCMODEOUT    {cMode = GetConsoleMode(hConsoleHandle, out uint lpMode)}");

            if (!cMode)
            {
                mode = 0;
                return false;
            }

            mode = (CONSOLE_MODE_OUTPUT)lpMode;
            return true;
        }

        public sealed class SurfaceItem : IEquatable<SurfaceItem>
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
            public SurfaceItem[,] Surface { get; internal set; }

            public override string ToString()
            {
                return $"CALC {CalculationTime}, RELE {ReleaseTime}, WRIT {WriteTime}, GLOB {GlobalTime}";
            }
        }

        private void Memset(byte[] array, int start, int count, byte value)
        {
            GCHandle h = default;
            try
            {
                h = GCHandle.Alloc(array, GCHandleType.Pinned);
                IntPtr addr = h.AddrOfPinnedObject() + start;
                MemsetDelegate(addr, value, count);
            }
            finally
            {
                if (h.IsAllocated)
                    h.Free();
            }
        }
    }
}
