using System;

using Ax.Engine.Core;
using Ax.Engine.Utils;
using Ax.Engine.ECS;
using Ax.Engine.ECS.Components;
using System.Diagnostics;

namespace Ax.Engine
{
    internal class Program
    {
        private static Game game;

        public static void Main(string[] __)
        {
            game = new GameBuilder()
                .SetTitle("Engine demo")
                .SetFont("Lucidas Console", 10, 10)
                .SetSize(80, 50)
                .SetPosition(5, 5)
                .SetCursorVisible(false)
                .LimitFPS(15)
                .SetRenderingMode(OutputHandler.RenderingMode.VTColorOnlyBackground)
                .Build();

            Color c = new Color(255, 0, 0);

            byte[] baseColorSequence = new byte[]
            {
                27, 91, 0, 56, 59, 50, 59, 0, 0, 0, 59, 0, 0, 0, 59, 0, 0, 0, 109
            };

            const int tries = 100000000;

            void TestMul()
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                for (int i = 0; i < tries; i++)
                {
                    byte[] colorSequence = new byte[19];
                    Buffer.BlockCopy(baseColorSequence, 0, colorSequence, 0, 19);

                    colorSequence[2] = 52;

                    // red
                    colorSequence[7] = (byte)(c.r * 0.01f % 10 + 48);
                    colorSequence[8] = (byte)(c.r * 0.10f % 10 + 48);
                    colorSequence[9] = (byte)(c.r % 10 + 48);
                                                   
                    // green                       
                    colorSequence[11] = (byte)(c.g * 0.01f % 10 + 48);
                    colorSequence[12] = (byte)(c.g * 0.10f % 10 + 48);
                    colorSequence[13] = (byte)(c.g % 10 + 48);
                                                   
                    // blue                        
                    colorSequence[15] = (byte)(c.b * 0.01f % 10 + 48);
                    colorSequence[16] = (byte)(c.b * 0.10f % 10 + 48);
                    colorSequence[17] = (byte)(c.b % 10 + 48);
                }

                stopwatch.Stop();
                Console.WriteLine(stopwatch.Elapsed);
            }

            void TestDiv()
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                for (int i = 0; i < tries; i++)
                {
                    byte[] colorSequence = new byte[19];
                    Buffer.BlockCopy(baseColorSequence, 0, colorSequence, 0, 19);

                    colorSequence[2] = 52;

                    // red
                    colorSequence[7] = (byte)(c.r / 100 % 10 + 48);
                    colorSequence[8] = (byte)(c.r / 010 % 10 + 48);
                    colorSequence[9] = (byte)(c.r / 001 % 10 + 48);

                    // green
                    colorSequence[11] = (byte)(c.g / 100 % 10 + 48);
                    colorSequence[12] = (byte)(c.g / 010 % 10 + 48);
                    colorSequence[13] = (byte)(c.g / 001 % 10 + 48);

                    // blue              
                    colorSequence[15] = (byte)(c.b / 100 % 10 + 48);
                    colorSequence[16] = (byte)(c.b / 010 % 10 + 48);
                    colorSequence[17] = (byte)(c.b / 001 % 10 + 48);
                }

                stopwatch.Stop();
                Console.WriteLine(stopwatch.Elapsed);
            }

            TestMul();
            TestDiv();

            /*
            Stopwatch a = new Stopwatch();
            a.Start();
            
            byte[] colorSequence = new byte[20];
            Buffer.BlockCopy(baseColorSequence, 0, colorSequence, 0, 20);

            colorSequence[2] = 52;

            // red
            colorSequence[7] = (byte)(c.r / 100 % 10 + 48);
            colorSequence[8] = (byte)(c.r / 010 % 10 + 48);
            colorSequence[9] = (byte)(c.r / 001 % 10 + 48);

            // green
            colorSequence[11] = (byte)(c.g / 100 % 10 + 48);
            colorSequence[12] = (byte)(c.g / 010 % 10 + 48);
            colorSequence[13] = (byte)(c.g / 001 % 10 + 48);

            // blue              
            colorSequence[15] = (byte)(c.b / 100 % 10 + 48);
            colorSequence[16] = (byte)(c.b / 010 % 10 + 48);
            colorSequence[17] = (byte)(c.b / 001 % 10 + 48);
            a.Stop();

            Native.WriteConsole(game.OutputHandler.Handle, colorSequence, 20, out _, new IntPtr(0));

            Console.WriteLine(a.Elapsed);

            Console.Read();
            */
            game.OpenDevMenu = false;

            EntityManager.EnableRegistry(false);

            CameraComponent camera = EntityManager.AddEntity().AddComponent<CameraComponent>();

            AnimatedSpriteComponent animatedCharacter = EntityManager.AddEntity().AddComponent<AnimatedSpriteComponent>();
            animatedCharacter.ImportSheet("assets/landscape", new Vector2Int(80, 50));
            animatedCharacter.animationDelay = 0;
            animatedCharacter.Transform.position = new Vector2(0, 0);

            /*IEnumerator TestMouseButtonCoroutine()
            {
                while(true)
                {
                    yield return new WaitForEndOfFrame();

                    Console.WriteLine("a");
                }
            }

            Yielder.StartCoroutine(TestMouseButtonCoroutine());
            */
            OutputHandler.RenderData average = new OutputHandler.RenderData();
            int iterations = 0;
            int iterationCount = 10;

            while (game.IsRunning)
            {
                game.HandleEvents();
                game.Update();
                game.Render();

                //Console.WriteLine(GameInput.GetMouseButtonDown(MOUSE_BUTTON.FROM_LEFT_1ST_BUTTON_PRESSED));

                iterations++;

                average.CalculationTime += game.OutputHandler.LastFrameData.CalculationTime;
                average.GlobalTime += game.OutputHandler.LastFrameData.GlobalTime;
                average.ReleaseTime += game.OutputHandler.LastFrameData.ReleaseTime;
                average.WriteTime += game.OutputHandler.LastFrameData.WriteTime;

                if (iterations >= iterationCount)
                {
                    Console.Title = average.ToString();
                    average = new OutputHandler.RenderData();
                    iterations = 0;
                }

                game.WaitFrame();
            }

            game.Clean();

            Console.ReadLine();
        }
    }
}
