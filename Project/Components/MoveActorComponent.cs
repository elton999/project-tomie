using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;

namespace Project.Components
{
    public class MoveActorComponent : Component
    {
        [ShowEditor] Vector2 _direction;
        [ShowEditor] float _velocity;

        private Actor _actor;

        public MoveActorComponent(float velocity) => SetVelocity(velocity);

        public override void Start()
        {
            if (GameObject is Actor)
                _actor = (Actor)GameObject;
            _actor ??= new Actor();
        }

        public override void UpdateData(GameTime gameTime)
        {
            _actor.Velocity = _direction * _velocity;
            base.UpdateData(gameTime);
        }

        public void SetDirection(Vector2 direction)
        {
            direction.Normalize();
            _direction = direction;
        }

        public void SetVelocity(float velocity) => _velocity = velocity;
    }
}
