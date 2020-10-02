using System;
using System.Drawing;
using System.Drawing.Imaging;

using Ax.Engine.Core;
using Ax.Engine.Utils;
using Ax.Engine.ECS;
using Ax.Engine.ECS.Components;

using static Ax.Engine.Core.Native;

using Color = Ax.Engine.Utils.Color;
using System.Threading;
using System.IO;

namespace Ax.Engine
{
    public sealed class Game
    {
        internal static Game Instance;

        public bool IsRunning { get; private set; } = false;
        public bool OpenDevMenu { get; set; } = false;

        public IntPtr HWND { get; private set; }
        public IntPtr HMENU { get; private set; }

        public int FrameCount { get; private set; } = 0;

        public OutputHandler OutputHandler { get; private set; }
        public InputHandler InputHandler { get; private set; }

        public static int WindowWidthInPixels { get; internal set; }
        public static int WindowHeightInPixels { get; internal set; }

        public static int WindowWidth { get; internal set; }
        public static int WindowHeight { get; internal set; }

        public static int FontWidth { get; internal set; }
        public static int FontHeight { get; internal set; }

        public Game(IntPtr hWnd, IntPtr hMenu, OutputHandler outputHandler, InputHandler inputHandler, bool isRunning) 
        {
            if (Instance != null)
            {
                Console.WriteLine("A game is already running !");
                return;
            }

            HWND = hWnd;
            HMENU = hMenu;

            OutputHandler = outputHandler;
            InputHandler = inputHandler;

            IsRunning = isRunning;

            Instance = this;
        }

        ~Game() { }

        // TODO: secure re call set or update realtime

        public void HandleEvents()
        {
            if (!IsRunning) { return; }

            // Capture events
            uint eventCount = InputHandler.Read(out INPUT_RECORD[] recs);
            FlushConsoleInputBuffer(InputHandler.Handle);

            /*
            if(eventCount == 0) { return; }

            
            Console.Clear();

            Console.WriteLine(eventCount);

            FieldInfo[] fields = typeof(INPUT_RECORD).GetFields();
            object[,] inputTable = new object[eventCount + 1, fields.Length + 14];

            int y = 1;
            for (int i = 0; i < fields.Length; i++)
            {
                FieldInfo[] subFields = fields[i].FieldType.GetFields();
                inputTable[0, y++] = $"== {fields[i].Name}";
                
                for (int j = 0; j < subFields.Length; j++)
                {
                    if(i != 0) { inputTable[0, y++] = subFields[j].Name; }
                    
                    for (int k = 0; k < eventCount; k++)
                    {
                        inputTable[k + 1, 0] = $"Event [{k + 1}]";
                        inputTable[k + 1, y - 1] = i == 0 ? fields[i].GetValue(recs[k]) : subFields[j].GetValue(fields[i].GetValue(recs[k]));
                    }
                }
            }

            Console.WriteLine(Logger.GenTable(inputTable));
            */

            for (int i = 0; i < eventCount; i++)
            {
                // Process built-in events
                switch(recs[i].EventType)
                {
                    // Keyboard
                    case 1:
                        switch(recs[i].KeyEvent.wVirtualKeyCode)
                        {
                            // F2
                            case 113:
                                if (OutputHandler.LastFrameData.Equals(default(OutputHandler.RenderData))) { continue; }

                                new Thread(() => TakeScreeshot(OutputHandler.LastFrameData.Surface)).Start();
                                break;
                        }

                        break;
                }
            }
        }

        public void TakeScreeshot(OutputHandler.SurfaceItem[,] surface)
        {
            if (!Directory.Exists("screenshots")) { Directory.CreateDirectory("screenshots"); }

            Bitmap bmp = new Bitmap(WindowWidthInPixels, WindowHeightInPixels);

            for (int y = 0; y < WindowHeight; y++)
            {
                for (int x = 0; x < WindowWidth; x++)
                {
                    for (int py = 0; py < FontHeight; py++)
                    {
                        for (int px = 0; px < FontWidth; px++)
                        {
                            bmp.SetPixel(x * FontWidth + px, y * FontHeight + py, Color.ToColor(surface[x, y]?.color ?? Color.Black));
                        }
                    }
                }
            }
            
            string outPath = Path.Combine("screenshots", string.Concat(DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-ff"), ".png"));
            bmp.Save(outPath, ImageFormat.Png);
        }

        public void Update()
        {
            if (!IsRunning) { return; }

            Yielder.ProcessCoroutines();
            EntityManager.Update();
            FrameCount++;
        }

        public void Render()
        {
            if (!IsRunning) { return; }

            OutputHandler.PrepareSurface(Console.BufferWidth, Console.BufferHeight);

            if (OpenDevMenu)
            {
                OutputHandler.RenderData lastFrame = OutputHandler.LastFrameData;

                string glob = lastFrame.GlobalTime.ToString();
                string calc = lastFrame.CalculationTime.ToString();
                string rele = lastFrame.ReleaseTime.ToString();
                string writ = lastFrame.WriteTime.ToString();

                OutputHandler.RenderStr(0, 0, int.MaxValue, "┌─────────────────────┐", Color.White, Color.Black, true);
                OutputHandler.RenderStr(0, 1, int.MaxValue, "│GLOB                 │", Color.White, Color.Black, true);
                OutputHandler.RenderStr(0, 2, int.MaxValue, "│CALC                 │", Color.White, Color.Black, true);
                OutputHandler.RenderStr(0, 3, int.MaxValue, "│RELE                 │", Color.White, Color.Black, true);
                OutputHandler.RenderStr(0, 4, int.MaxValue, "│WRIT                 │", Color.White, Color.Black, true);
                OutputHandler.RenderStr(0, 5, int.MaxValue, "└─────────────────────┘", Color.White, Color.Black, true);

                OutputHandler.RenderStr(6, 1, int.MaxValue, glob, Color.White, Color.Black, true);
                OutputHandler.RenderStr(6, 2, int.MaxValue, calc, Color.White, Color.Black, true);
                OutputHandler.RenderStr(6, 3, int.MaxValue, rele, Color.White, Color.Black, true);
                OutputHandler.RenderStr(6, 4, int.MaxValue, writ, Color.White, Color.Black, true);
            }

            if (!EntityManager.EntityExistsWithComponent<CameraComponent>())
            {
                OutputHandler.RenderStr(Console.WindowWidth / 2 - 9, MathHelper.FloorToInt(Console.WindowHeight / 2f) - 1, int.MaxValue, "NO CAMERA RENDERING", Color.White, Color.Black, true);
            }
            else
            {
                EntityManager.Render(OutputHandler);
            }

            OutputHandler.WaitFrame();

            OutputHandler.ReleaseSurface();
        }

        public bool Clean()
        {
            if (!IsRunning) { return false; }

            IsRunning = !(InputHandler.Disable() && OutputHandler.Disable());

            /*_cursorKeysMode = VTCursorKeysMode.Normal;
            _keypadMode = VTKeypadMode.Numeric;*/

            Console.WriteLine("Game cleaned.");
            Console.ReadKey();

            Environment.Exit(IsRunning ? 1 : 0);

            return !IsRunning;
        }        
    }
}
