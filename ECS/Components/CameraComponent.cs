using Ax.Engine.Utils;

namespace Ax.Engine.ECS.Components
{
    [UniqueComponent]
    public sealed class CameraComponent : Component
    {
        public Vector2 Rect;
        public int RenderPriority = 0;
    }
}
