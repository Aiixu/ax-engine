﻿using System;
using System.Linq;
using Ax.Engine.Core;
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
                .SetFont("Lucidas Console", 8, 16)
                .SetSize(100, 30)
                .SetPosition(10, 10)
                .SetCursorVisible(false)
                .Build();

            game.OpenDevMenu = true;

            CameraComponent camera = EntityManager.AddEntity().AddComponent<CameraComponent>();

            Logger.Write(camera.Entity.HasComponent<CameraComponent>());

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
