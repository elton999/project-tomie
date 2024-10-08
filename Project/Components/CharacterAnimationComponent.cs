﻿using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Components
{
    public class CharacterAnimationComponent : Component
    {
        private const string WALK_ANIMATION = "walk";
        private const string IDLE_ANIMATION = "idle";

        private ISpriteAnimation _animation;
        private MoveActorComponent _moveActor;

        public override void Start() => _moveActor = GameObject.GetComponent<MoveActorComponent>();

        public void AddAnimation(string path)
        {
            _animation = new AsepriteAnimation(GameObject.Scene.Content.Load<AsepriteDefinitions>(path));
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (_animation == null) return;

            string animationName = IDLE_ANIMATION;

            if (_moveActor.IsMoving)
                animationName = WALK_ANIMATION;

            if (_moveActor.Direction.X > 0.0f && _moveActor.IsMoving)
                GameObject.spriteEffect = SpriteEffects.None;
            if (_moveActor.Direction.X < 0.0f && _moveActor.IsMoving)
                GameObject.spriteEffect = SpriteEffects.FlipHorizontally;

            _animation.Play(deltaTime, animationName, AnimationDirection.LOOP);
            GameObject.Body = _animation.Body;
        }
    }
}
