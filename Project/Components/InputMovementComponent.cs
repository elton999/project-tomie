using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Input;

namespace Project.Components
{
    public class InputMovementComponent : Component
    {
        private MoveActorComponent _moveActor;
        [ShowEditor] private bool _inputEnable = true;
        [ShowEditor] private bool _verticalMovement = true;

        public override void Start()
        {
            _moveActor = GameObject.GetComponent<MoveActorComponent>();
        }

        public override void Update(float deltaTime)
        {
            if (!_inputEnable) return;

            Vector2 direction = Vector2.Zero;

            if (KeyBoardHandler.KeyDown(Input.LEFT))
                direction += Vector2.UnitX * -1;
            if (KeyBoardHandler.KeyDown(Input.RIGHT))
                direction += Vector2.UnitX;

            if (KeyBoardHandler.KeyDown(Input.UP) && _verticalMovement)
                direction += Vector2.UnitY * -1;
            if (KeyBoardHandler.KeyDown(Input.DOWN) && _verticalMovement)
                direction += Vector2.UnitY;

            if (direction.Length() > 0) direction.Normalize();
            _moveActor.SetDirection(direction);
            base.Update(deltaTime);
        }

        public void DisableInput() => _inputEnable = false;
        public void EnableInput() => _inputEnable = true;
    }
}
