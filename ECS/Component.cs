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
    }
}
