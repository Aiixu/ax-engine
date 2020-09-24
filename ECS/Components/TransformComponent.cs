using Ax.Engine.Utils;

namespace Ax.Engine.ECS.Components
{
    public sealed class TransformComponent : Component
    {
        public Vector2 position = Vector2.Zero;
        public Vector2 scale = Vector2.One;

        public TransformComponent()
        {
            position = Vector2.Zero;
        }
    }
}
