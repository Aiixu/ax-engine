using Ax.Engine.Utils;

namespace Ax.Engine.ECS.Components
{
    internal sealed class ColliderComponent : Component
    {
        private Rect collider;

        public Vector2 size = Vector2.One;
        public string collisionLayer = "EVERYTHING";
        public bool isTrigger = false;

        public override void Update()
        {
            collider.size = Transform.scale * size;
            collider.position = Transform.position - collider.size / 2;
        }
    }
}
