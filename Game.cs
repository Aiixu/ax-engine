using System;
using System.Reflection;

using Ax.Engine.Core;
using Ax.Engine.Utils;
using Ax.Engine.ECS;
using Ax.Engine.ECS.Components;

using static Ax.Engine.Core.Native;

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

            Console.SetCursorPosition(0, 0);

            if(eventCount == 0) { return; }
            Console.Clear();

            Console.WriteLine(eventCount);

            FieldInfo[] fields = typeof(INPUT_RECORD).GetFields();
            object[,] inputTable = new object[eventCount + 1, fields.Length + 13];

            int y = 1;
            for (int i = 1; i < fields.Length; i++)
            {
                FieldInfo[] subFields = fields[i].FieldType.GetFields();
                inputTable[0, y++] = $"== {fields[i].Name}";
                
                for (int j = 0; j < subFields.Length; j++)
                {
                    inputTable[0, y++] = subFields[j].Name;

                    for (int k = 0; k < eventCount; k++)
                    {
                        inputTable[k + 1, 0] = $"Event [{k + 1}]";
                        inputTable[k + 1, y - 1] = subFields[j].GetValue(fields[i].GetValue(recs[k]));
                    }
                }
            }

            Console.WriteLine(Logger.GenTable(inputTable));

            return;
            
            for (int i = 0; i < 1; i++) // recs.Length
            {
                INPUT_RECORD rec = recs[i];

                Console.WriteLine(rec.EventType);

                Console.WriteLine("\n- FocusEvent -");
                Console.WriteLine("bSetFocus :\t\t" + rec.FocusEvent.bSetFocus);

                Console.WriteLine("\n- KeyEvent -");
                Console.WriteLine("bKeyDown :\t\t" + rec.KeyEvent.bKeyDown);
                Console.WriteLine("dwControlKeyState :\t" + rec.KeyEvent.dwControlKeyState);
                Console.WriteLine("UnicodeChar :\t\t" + rec.KeyEvent.UnicodeChar);
                Console.WriteLine("wRepeatCount :\t\t" + rec.KeyEvent.wRepeatCount);
                Console.WriteLine("wVirtualKeyCode :\t" + rec.KeyEvent.wVirtualKeyCode);
                Console.WriteLine("wVirtualScanCode:\t" + rec.KeyEvent.wVirtualScanCode);

                Console.WriteLine("\n- MenuEvent -");
                Console.WriteLine("dwCommandId :\t\t" + rec.MenuEvent.dwCommandId);

                Console.WriteLine("\n- MouseEvent -");
                Console.WriteLine("dwButtonState :\t\t" + rec.MouseEvent.dwButtonState);
                Console.WriteLine("dwControlKeyState :\t" + rec.MouseEvent.dwControlKeyState);
                Console.WriteLine("dwEventFlags :\t\t" + rec.MouseEvent.dwEventFlags);
                Console.WriteLine("dwMousePosition :\t" + rec.MouseEvent.dwMousePosition);

                Console.WriteLine("\n- WindowBufferSizeEvent -");
                Console.WriteLine("dwSize :\t\t" + rec.WindowBufferSizeEvent.dwSize);
            }

            // Handle built-in events

        }

        public void Update()
        {
            if (!IsRunning) { return; }

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
