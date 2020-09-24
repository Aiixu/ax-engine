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
                .SetFont("Lucidas Console", 20, 20)
                .SetSize(60, 40)
                .SetPosition(10, 10)
                .SetCursorVisible(false)
                .Build();

            game.OpenDevMenu = true;

            CameraComponent camera = EntityManager.AddEntity().AddComponent<CameraComponent>();

            SpriteComponent character = EntityManager.AddEntity().AddComponent<SpriteComponent>();
            character.texture = (Bitmap)Image.FromFile("assets/character/character-1.png");
            character.Transform.position = new Vector2(10, 10);
            character.Entity.SetActive(false);

            AnimatedSpriteComponent animatedCharacter = EntityManager.AddEntity().AddComponent<AnimatedSpriteComponent>();
            animatedCharacter.ImportSheet("assets/character-new", new Vector2Int(40, 40));
            animatedCharacter.animationDelay = 70;
            animatedCharacter.Transform.position = new Vector2(5, 0);

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
