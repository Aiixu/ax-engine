using System;

using Ax.Engine.Utils;
using Ax.Engine.ECS;
using Ax.Engine.ECS.Components;
using Ax.Engine.Core.Rendering;

namespace Ax.Engine
{
    internal class Program
    {
        private static Game game;

        public static void Main(string[] _)
        {
            game = new GameBuilder()
                .SetTitle("Engine demo")
                .SetFont("Lucidas Console", 10, 10)
                .SetSize(120, 50)
                .SetPosition(5, 5)
                .SetCursorVisible(false)
                .LimitFPS(30)
                .SetRenderer<SimpleColorOnlySurfaceRenderer, SingleBufferedOutputHandler>()
                .Build();

            game.OpenDevMenu = false;

            EntityManager.EnableRegistry(false);

            // Create camera
            EntityManager.AddEntity().AddComponent<CameraComponent>();

            AnimatedSpriteComponent animatedCharacter = EntityManager.AddEntity().AddComponent<AnimatedSpriteComponent>();
            animatedCharacter.ImportSheet("assets/landscape", new Vector2Int(120, 50));
            animatedCharacter.animationDelay = 50;
            animatedCharacter.Transform.position = new Vector2(0, 0);
                        
            int iterations = 0;
            int iterationCount = 10;

            RenderData average = new RenderData();

            while (game.IsRunning)
            {
                game.HandleEvents();
                game.Update();
                game.Render();

                iterations++;

                average.CalculationTime += game.Renderer.LastRenderData.CalculationTime;
                average.GlobalTime += game.Renderer.LastRenderData.GlobalTime;
                average.ReleaseTime += game.Renderer.LastRenderData.ReleaseTime;
                average.WriteTime += game.Renderer.LastRenderData.WriteTime;

                if (iterations >= iterationCount)
                {
                    Console.Title = average.ToString();
                    average = new RenderData();
                    iterations = 0;
                }

                game.WaitFrame();
            }

            game.Clean();

            Console.ReadLine();
        }
    }
}
