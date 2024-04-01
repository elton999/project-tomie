using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Input;

namespace Project.Components
{
    public class InputMovementComponent : Component
    {
        private MoveActorComponent _moveActor;

        public override void Start()
        {
            _moveActor = GetComponent<MoveActorComponent>();
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 direction = Vector2.Zero;
            if (KeyBoardHandler.KeyDown(Input.LEFT))
                direction = Vector2.UnitX * -1;
            if (KeyBoardHandler.KeyDown(Input.RIGHT))
                direction = Vector2.UnitX;

            _moveActor.SetDirection(direction);
            base.Update(gameTime);
        }
    }
}
