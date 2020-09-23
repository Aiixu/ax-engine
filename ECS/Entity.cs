using Ax.Engine.Core;
using Ax.Engine.ECS.Components;

using System;
using System.Collections.Generic;

namespace Ax.Engine.ECS
{
    public sealed class Entity
    {
        public const int MAX_COMPONENTS_COUNT = 32;

        public TransformComponent Transform { get; internal set; }

        private readonly List<Component> componentsArray = new List<Component>(MAX_COMPONENTS_COUNT);
        private readonly Dictionary<Type, bool> componentsRegistry = new Dictionary<Type, bool>();

        public bool IsActive { get; private set; }
        public int ReferenceId { get; internal set; }

        public void Update()
        {
            for (int i = 0; i < componentsArray.Count; i++)
            {
                componentsArray[i].Update();
            }
        }

        public void Render(ref OutputHandler.SurfaceItem[,] surface)
        {
            for (int i = 0; i < componentsArray.Count; i++)
            {
                componentsArray[i].Render(ref surface);
            }
        }

        public bool HasComponent<T>() where T : Component
        {
            Type typeofT = typeof(T);
            return componentsRegistry.ContainsKey(typeofT) && componentsRegistry[typeofT];
        }

        public bool HasComponent<T>(out T c) where T: Component
        {
            return (c = GetComponent<T>()) != null;
        }

        public T AddComponent<T>() where T: Component, new()
        {
            T c = new T
            {
                Entity = this
            };
            componentsArray.Add(c);

            c.Transform = Transform;
            c.Init();

            componentsRegistry[typeof(T)] = true;

            return (T)c;
        }

        public T GetComponent<T>() where T: Component
        {
            return HasComponent<T>() ? (T)componentsArray.Find(c => c.GetType() == typeof(T)) : null;
        }
        
        public void SetActive(bool active)
        {
            IsActive = active;
        }
    }
}
