using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Input;

namespace Project.Components
{
    public class InputMovementComponent : Component
    {
        private MoveActorComponent _moveActor;
        [ShowEditor] private bool _inputEnable = true;

        public override void Start()
        {
            _moveActor = GameObject.GetComponent<MoveActorComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            if (!_inputEnable) return;

            Vector2 direction = Vector2.Zero;
            if (KeyBoardHandler.KeyDown(Input.LEFT))
                direction = Vector2.UnitX * -1;
            if (KeyBoardHandler.KeyDown(Input.RIGHT))
                direction = Vector2.UnitX;

            _moveActor.SetDirection(direction);
            base.Update(gameTime);
        }

        public void DisableInput() => _inputEnable = false;
        public void EnableInput() => _inputEnable = true;
    }
}
