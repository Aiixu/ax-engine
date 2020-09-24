using Ax.Engine.Core;
using Ax.Engine.ECS.Components;

namespace Ax.Engine.ECS
{
    public class Component
    {
        public Entity Entity { get; internal set; }

        public TransformComponent Transform { get; internal set; }

        public virtual void Init() { }
        public virtual void Update() { }
        public virtual void Render(ref OutputHandler.SurfaceItem[,] surface) { }

        // Entity implementation
        public bool HasComponent<T>() where T : Component => Entity.HasComponent<T>();
        public bool HasComponent<T>(out T c) where T : Component => Entity.HasComponent(out c);
        public T AddComponent<T>() where T : Component, new() => Entity.AddComponent<T>();
        public T GetComponent<T>() where T : Component => Entity.GetComponent<T>();
        public void DestroyComponent<T>() where T : Component => Entity.DestroyComponent<T>();
    }
}
