using System;

using Ax.Engine.Utils;
using Ax.Engine.ECS;
using Ax.Engine.ECS.Components;
using Ax.Engine.Core.Native;
using Ax.Engine.Core.Rendering;
using System.Diagnostics;

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
                .SetWindowSize(90, 35)
                .SetPosition(5, 5)
                .SetCursorVisible(false)
                .LimitFPS(200)
                .SetRenderer<SimpleColorOnlySurfaceRenderer, DoubleBufferedOutputHandler>()
                .Build();

            game.OpenDevMenu = false;

            EntityManager.EnableRegistry(false);

            // Create camera
            EntityManager.AddEntity().AddComponent<CameraComponent>();
            
            AnimatedSpriteComponent animatedCharacter = EntityManager.AddEntity().AddComponent<AnimatedSpriteComponent>();
            animatedCharacter.ImportSheet("assets/landscape", new Vector2Int(90, 35));
            animatedCharacter.animationDelay = 0;
            animatedCharacter.Transform.position = new Vector2(0, 0);
            
            // ProcessRendererComponent processRenderer = EntityManager.AddEntity().AddComponent<ProcessRendererComponent>();
            // processRenderer.AttachProcess(Process.GetProcessById(6936).Handle);
            
            int iterations = 0;
            int maxInterations = 20;
            
            while (game.IsRunning)
            {
                game.StartFrame();

                game.HandleEvents();
                game.Update();
                game.Render();

                game.WaitFrame();

                game.EndFrame();

                if(iterations++ >= maxInterations)
                {
                    WinApi.SetConsoleTitle("FPS : " + Math.Ceiling(1000 / game.FrameDuration.TotalMilliseconds));
                    iterations = 0;
                }
            }

            game.Clean();

            Console.ReadLine();
        }
    }
}
