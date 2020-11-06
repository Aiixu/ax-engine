using System;
using System.IO;
using System.Threading;

using Ax.Engine.Core;
using Ax.Engine.ECS;
using Ax.Engine.ECS.Components;
using Ax.Engine.Core.Rendering;

using static Ax.Engine.Core.Native.WinApi;
using static Ax.Engine.Core.Native.WinUser;

namespace Ax.Engine
{
    public sealed class Game
    {
        internal static Game Instance;

        public bool IsRunning { get; private set; } = false;
        public bool OpenDevMenu { get; set; } = false;

        public int FrameCount { get; private set; } = 0;

        public InputHandler Input { get; private set; }
        public SurfaceRenderer Renderer { get; private set; }

        public Game(SurfaceRenderer renderer, InputHandler inputHandler) 
        {
            if (Instance != null)
            {
                Console.WriteLine("A game is already running !");
                return;
            }

            Renderer = renderer;
            Input = inputHandler;

            IsRunning = true;

            Instance = this;
        }

        ~Game() { }

        // TODO: secure re call set or update realtime

        public void HandleEvents()
        {
            if (!IsRunning) { return; }

            // Capture events
            Input.Peek(out INPUT_RECORD[] recs);
            FlushConsoleInputBuffer(Input.Handle);

            Input.UpdateInputStates(recs);
            //InputHandler.Read(out _);
            //FlushConsoleInputBuffer(IutputHandler.Handle);
            //InputHandler.Read()

            GameInput.InvokeEvents();

            // Built-in event handling
            if (GameInput.GetKeyDown(KEY.F2) && FrameCount > 1)
            {
                new Thread(TakeScreenshot).Start();
            }

            return;
        }

        public void TakeScreenshot()
        {
            if (!Directory.Exists("screenshots")) { Directory.CreateDirectory("screenshots"); }

            string outPath = Path.Combine("screenshots", string.Concat(DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-ff"), ".png"));
            Imaging.CaptureWindowToFile(GetConsoleWindow(), outPath);
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

            Renderer.PrepareSurface();

            if (OpenDevMenu)
            {/*
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
                OutputHandler.RenderStr(6, 4, int.MaxValue, writ, Color.White, Color.Black, true);*/
            }

            if (!EntityManager.EntityExistsWithComponent<CameraComponent>())
            {
                //OutputHandler.RenderStr(Console.WindowWidth / 2 - 9, MathHelper.FloorToInt(Console.WindowHeight / 2f) - 1, int.MaxValue, "NO CAMERA RENDERING", Color.White, Color.Black, true);
            }
            else
            {
                EntityManager.Render(Renderer);
            }

            Renderer.ReleaseSurface();
        }

        public void WaitFrame()
        {
            Renderer.OutputHandler.WaitFrame();
        }

        public void Clean()
        {
            if (!IsRunning)
            {
                Environment.Exit(1);
                return; 
            }

            Input.Disable();
            Renderer.OutputHandler.Disable();

            /*_cursorKeysMode = VTCursorKeysMode.Normal;
            _keypadMode = VTKeypadMode.Numeric;*/

            Console.WriteLine("Game cleaned.");
            Console.ReadKey();

            Environment.Exit(IsRunning ? 1 : 0);

            return;
        }        
    }
}
