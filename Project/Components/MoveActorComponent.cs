using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;

namespace Project.Components
{
    public class MoveActorComponent : Component
    {
        [ShowEditor] Vector2 _direction = Vector2.Zero;
        [ShowEditor] float _velocity = 1f;

        private Actor _actor;

        public override void Start() => _actor = GameObject.GetActor();

        public override void UpdateData(GameTime gameTime)
        {
            _actor.Velocity = _direction * _velocity;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void SetVelocity(float velocity) => _velocity = velocity;
    }
}
