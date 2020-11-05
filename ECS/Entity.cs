using Ax.Engine.Core;
using Ax.Engine.Core.Rendering;
using Ax.Engine.ECS.Components;

using System;
using System.Collections.Generic;

namespace Ax.Engine.ECS
{
    public sealed class Entity
    {
        public const int MAX_COMPONENTS_COUNT = 32;

        public TransformComponent Transform { get; internal set; }

        internal Dictionary<Type, bool> ComponentsRegistry { get; } = new Dictionary<Type, bool>();
        internal List<Component> Components { get; } = new List<Component>(MAX_COMPONENTS_COUNT);

        public bool IsActive { get; private set; } = true;
        public int ReferenceId { get; internal set; }

        public void Update()
        {
            for (int i = 0; i < Components.Count; i++)
            {
                Components[i].Update();
            }
        }

        public void Render(SurfaceRenderer renderer)
        {
            for (int i = 0; i < Components.Count; i++)
            {
                Components[i].Render(renderer);
            }
        }

        public bool HasComponent<T>() where T : Component
        {
            Type typeofT = typeof(T);
            return ComponentsRegistry.ContainsKey(typeofT) && ComponentsRegistry[typeofT];
        }

        public bool HasComponent<T>(out T c) where T: Component
        {
            return (c = GetComponent<T>()) != null;
        }

        public T AddComponent<T>() where T: Component, new()
        {
            Type typeofT = typeof(T);

            if (!ComponentsRegistry.ContainsKey(typeofT))
            {
                EntityManager.RegisterAddEntityComponent(typeofT, this);
            }
            else if (typeofT.IsDefined(typeof(UniqueComponentAttribute), true))
            {
                throw new Exception("Trying to adding a unique component to an entity but this entity already have the component.");
            }

            T c = new T { Entity = this };
            Components.Add(c);

            c.Transform = Transform;
            c.Init();

            ComponentsRegistry[typeofT] = true;

            return c;
        }

        public T GetComponent<T>() where T: Component
        {
            return HasComponent<T>() ? (T)Components.Find(c => c.GetType() == typeof(T)) : null;
        }

        public void DestroyComponent<T>() where T : Component
        {
            Type typeofT = typeof(T);

            Components.RemoveAll(c => c.GetType() == typeofT);
            ComponentsRegistry[typeofT] = false;

            EntityManager.RegisterDestroyEntityComponent(typeofT, this);
        }
        
        public void SetActive(bool active)
        {
            IsActive = active;
        }
    }
}
