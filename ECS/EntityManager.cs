using System;
using System.Collections.Generic;
using System.Linq;
using Ax.Engine.Core;
using Ax.Engine.ECS.Components;

namespace Ax.Engine.ECS
{
    public class EntityManager
    {
        private static int LastEntityId = 0;

        private readonly List<Entity> entities = new List<Entity>();

        private static EntityManager instance;

        public EntityManager()
        {
            if(instance != null) { throw new Exception("An entity manager is already running"); }

            instance = this;
        }

        public void Update()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update();
            }
        }

        public void Render(ref OutputHandler.SurfaceItem[,] surface)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Render(ref surface);
            }
        }

        public static IEnumerable<Entity> FindEntitiesWithComponent<T>() where T: Component
        {
            return instance.entities.Where(e => e.IsActive && e.HasComponent<T>());
        }

        public Entity AddEntity()
        {
            Entity e = new Entity();
            e.Transform = e.AddComponent<TransformComponent>();
            e.ReferenceId = ++LastEntityId;

            entities.Add(e);

            return e;
        }

        public void Destroy(Entity e)
        {
            if (!entities.Contains(e)) { return; }

            entities.Remove(e);
        }

        public void Destroy(int referenceId)
        {
            Destroy(entities.Find(e => e.ReferenceId == referenceId));
        }
    }
}
