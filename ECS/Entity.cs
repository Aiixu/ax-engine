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

        internal Dictionary<Type, bool> componentsRegistry { get; } = new Dictionary<Type, bool>();
        internal List<Component> _components { get; } = new List<Component>(MAX_COMPONENTS_COUNT);

        public bool IsActive { get; private set; }
        public int ReferenceId { get; internal set; }

        public void Update()
        {
            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].Update();
            }
        }

        public void Render(ref OutputHandler.SurfaceItem[,] surface)
        {
            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].Render(ref surface);
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
            Type typeofT = typeof(T);

            if (!componentsRegistry[typeofT])
            {
                EntityManager.RegisterAddEntityComponent(typeofT, this);
            }
            else if (typeofT.IsDefined(typeof(UniqueComponentAttribute), true))
            {
                throw new Exception("Trying to adding a unique component to an entity but this entity already have the component.");
            }

            T c = new T { Entity = this };
            _components.Add(c);

            c.Transform = Transform;
            c.Init();

            componentsRegistry[typeofT] = true;

            return c;
        }

        public T GetComponent<T>() where T: Component
        {
            return HasComponent<T>() ? (T)_components.Find(c => c.GetType() == typeof(T)) : null;
        }

        public void DestroyComponent<T>() where T : Component
        {
            Type typeofT = typeof(T);

            _components.RemoveAll(c => c.GetType() == typeofT);
            componentsRegistry[typeofT] = false;

            EntityManager.RegisterDestroyEntityComponent(typeofT, this);
        }
        
        public void SetActive(bool active)
        {
            IsActive = active;
        }
    }
}
