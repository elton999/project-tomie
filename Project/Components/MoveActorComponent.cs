using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;

namespace Project.Components
{
    public class MoveActorComponent : Component
    {
        [ShowEditor] private Vector2 _direction = Vector2.Zero;
        [ShowEditor] private float _velocity = 1f;

        private Actor _actor;

        public bool IsMoving => _direction.LengthSquared() > 0.0f;
        public Vector2 Direction => _direction;

        public override void Start() => _actor = GameObject.GetActor();

        public override void UpdateData(float deltaTime) => _actor.Velocity = _direction * _velocity;

        public void SetDirection(Vector2 direction) => _direction = direction;

        public void SetVelocity(float velocity) => _velocity = velocity;
    }
}
