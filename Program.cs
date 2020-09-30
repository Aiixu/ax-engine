using System;

using Ax.Engine.Core;
using Ax.Engine.Utils;
using Ax.Engine.ECS;
using Ax.Engine.ECS.Components;
using System.Linq;

namespace Ax.Engine
{
    internal class Program
    {
        private static Game game;

        public static void Main(string[] _)
        {
            game = new GameBuilder()
                .SetTitle("Engine demo")
                .SetFont("Lucidas Console", 8, 16)
                .SetSize(60, 40)
                .SetPosition(10, 10)
                .SetCursorVisible(false)
                .LimitFPS(11)
                .SetRenderingMode(OutputHandler.RenderingMode.VTColorOnlyBackground)
                .Build();

            game.OpenDevMenu = false;

            EntityManager.EnableRegistry(false);

            CameraComponent camera = EntityManager.AddEntity().AddComponent<CameraComponent>();

            AnimatedSpriteComponent animatedCharacter = EntityManager.AddEntity().AddComponent<AnimatedSpriteComponent>();
            animatedCharacter.ImportSheet("assets/character-new", new Vector2Int(40, 40));
            animatedCharacter.animationDelay = 0;
            animatedCharacter.Transform.position = new Vector2(5, 0);

            object[,] args = new object[,]
                {
                    { "Col1", "Column 2", "C3", "Colmun number 3" },
                    { "arg1", "argument 1", "aaaaaaaaaaa", "a" },
                    { "arg1", "argument 1", "aaaaaaaaaaa", "aaaaaaaaaaaaaaaaaaaaaaaa" },
                    { "aaaaaa", " 1", "aaaaaaaaaaa", "j" }
                };

            Console.WriteLine(Logger.GenTable(args));

            Console.Read();
            Environment.Exit(0);


            while (game.IsRunning)
            {
                game.HandleEvents();
                //game.Update();
                //game.Render();
            }

            game.Clean();

            Console.ReadLine();
        }
    }
}
