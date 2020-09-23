using System;
using System.Collections.Generic;
using System.Linq;
using Ax.Engine.Core;
using Ax.Engine.ECS.Components;

namespace Ax.Engine.ECS
{
    public static class EntityManager
    {
        private static int LastEntityId = 0;

        private static readonly List<Entity> entities = new List<Entity>();
        private static readonly Dictionary<Type, List<Entity>> componentsRegistry = new Dictionary<Type, List<Entity>>();

        private static bool registryEnabled;

        public static void Update()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update();
            }
        }

        public static void Render(ref OutputHandler.SurfaceItem[,] surface)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Render(ref surface);
            }
        }

        public static Entity AddEntity()
        {
            Entity e = new Entity();
            e.Transform = e.AddComponent<TransformComponent>();
            e.ReferenceId = ++LastEntityId;

            entities.Add(e);

            return e;
        }

        public static void Destroy(Entity e)
        {
            if (!entities.Contains(e)) { return; }

            entities.Remove(e);
        }

        public static void Destroy(int referenceId)
        {
            Destroy(entities.Find(e => e.ReferenceId == referenceId));
        }

        public static IEnumerable<Entity> FindEntitiesWithComponent<T>() where T : Component
        {
            return entities.Where(e => e.IsActive && e.HasComponent<T>());
        }

        public static void EnableRegistry(bool regenerate)
        {
            registryEnabled = true;

            if(regenerate)
            {
                componentsRegistry.Clear();

                foreach (Entity entity in entities)
                {
                    foreach (Component component in entity._components)
                    {

                    }
                }
            }
        }

        public static void DisableRegistry()
        {
            registryEnabled = false;
        }

        internal static void RegisterAddEntityComponent(Type component, Entity entity)
        {
            if(!componentsRegistry.ContainsKey(component))
            {
                componentsRegistry.Add(component, new List<Entity>());
            }

            componentsRegistry[component].Add(entity);
        }

        internal static void RegisterDestroyEntityComponent(Type component, Entity entity)
        {
            componentsRegistry[component].Remove(entity);
        }
    }
}
