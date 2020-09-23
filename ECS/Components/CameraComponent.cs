using Ax.Engine.Utils;

namespace Ax.Engine.ECS.Components
{
    public sealed class CameraComponent : Component
    {
        public Vector2 Rect;
        public int RenderPriority = 0;
    }
}
