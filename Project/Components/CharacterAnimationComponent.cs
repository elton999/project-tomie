using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;

namespace Project.Components
{
    public class CharacterAnimationComponent : Component
    {
        private const string WALK_ANIMATION = "walk";
        private const string IDLE_ANIMATION = "idle";

        private AsepriteAnimation _animation;
        private MoveActorComponent _moveActor;

        public override void Start() => _moveActor = GetComponent<MoveActorComponent>();

        public void AddAnimation(string path)
        {
            _animation = new AsepriteAnimation(GameObject.Scene.Content.Load<AsepriteDefinitions>(path));
        }

        public override void Update(GameTime gameTime)
        {
            if (_animation == null) return;

            string animationName = IDLE_ANIMATION;

            if (_moveActor.IsMoving)
                animationName = WALK_ANIMATION;

            _animation.Play(gameTime, animationName, AsepriteAnimation.AnimationDirection.LOOP);
            GameObject.Body = _animation.Body;
        }
    }
}
