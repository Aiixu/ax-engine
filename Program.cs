using System;

using Ax.Engine.Core;
using Ax.Engine.Utils;
using Ax.Engine.ECS;
using Ax.Engine.ECS.Components;

using static Ax.Engine.Core.Native;

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
            while (game.IsRunning)
            {
                game.HandleEvents();
                game.Update();
                //game.Render();

                Console.WriteLine(GameInput.GetMouseButton(MOUSE_BUTTON.FROM_LEFT_1ST_BUTTON_PRESSED));

                game.WaitFrame();
            }

            game.Clean();

            Console.ReadLine();
        }
    }
}
