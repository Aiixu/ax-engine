using System;
using System.Drawing;

using Ax.Engine.Utils;
using Ax.Engine.ECS;
using Ax.Engine.ECS.Components;

namespace Ax.Engine
{
    internal class Program
    {
        private static Game game;

        public static void Main(string[] _)
        {
            game = new GameBuilder()
                .SetTitle("Engine demo")
                .SetFont("Lucidas Console", 16, 16)
                .SetSize(50, 30)
                .SetPosition(10, 10)
                .SetCursorVisible(false)
                .Build();

            game.OpenDevMenu = true;

            CameraComponent camera = EntityManager.AddEntity().AddComponent<CameraComponent>();

            SpriteComponent character = EntityManager.AddEntity().AddComponent<SpriteComponent>();
            character.texture = (Bitmap)Image.FromFile("assets/character/character-1.png");
            character.Transform.position = new Vector2(10, 10);

            while (game.IsRunning)
            {
                game.HandleEvents();
                game.Update();
                game.Render();
            }

            game.Clean();

            Console.ReadLine();
        }
    }
}
