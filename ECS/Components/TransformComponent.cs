using Ax.Engine.Utils;

namespace Ax.Engine.ECS.Components
{
    public sealed class TransformComponent : Component
    {
        public Vector2 position;
        public Vector2 scale;

        public TransformComponent()
        {
            position = Vector2.Zero;
        }
    }
}
