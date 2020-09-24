using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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

        public static void EnableRegistry(bool regenerate)
        {
            registryEnabled = true;

            if(regenerate)
            {
                componentsRegistry.Clear();

                for (int i = 0; i < entities.Count; i++)
                {
                    Entity entity = entities[i];

                    for (int j = 0; j < entity.Components.Count; j++)
                    {
                        Type componentType = entity.Components[i].GetType();

                        if(!componentsRegistry.ContainsKey(componentType))
                        {
                            componentsRegistry.Add(componentType, new List<Entity>());
                        }
                        else if(componentsRegistry[componentType].Contains(entity))
                        {
                            continue;
                        }

                        componentsRegistry[componentType].Add(entity);
                    }
                }
            }
        }

        public static void DisableRegistry()
        {
            registryEnabled = false;
        }

        public static bool EntityExistsWithComponent<T>() where T: Component
        {
            Type typeofT = typeof(T);
            return registryEnabled ? componentsRegistry.ContainsKey(typeofT) && CountEntitiesWithComponent<T>() != 0 : entities.Any(e => e.IsActive && e.HasComponent<T>());
        }

        public static int CountEntitiesWithComponent<T>() where T: Component
        {
            Type typeofT = typeof(T);
            return registryEnabled ? componentsRegistry.TryGetValue(typeofT, out List<Entity> registryEntities) ? registryEntities.Count : 0 : FindEntitiesWithComponent<T>().Count();
        }

        public static IEnumerable<Entity> FindEntitiesWithComponent<T>() where T : Component
        {
            Type typeofT = typeof(T);
            return registryEnabled ? componentsRegistry.TryGetValue(typeofT, out List<Entity> registryEntities) ? registryEntities : null : entities.Where(e => e.IsActive && e.HasComponent<T>());
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
