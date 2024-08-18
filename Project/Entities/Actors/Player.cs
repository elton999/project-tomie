﻿using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using Project.Components;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Entities.Actors
{
    public class Player : Actor
    {
        private static InputMovementComponent _inputMovement;

        public override void Start()
        {
            size = new Point(20, 20);
            Origin = new Vector2(20, 46);

            HasGravity = false;

            tag = "player";
            base.Start();

            Sprite = Scene.Content.Load<Texture2D>(FilePath.PLAYER_SPRITE_PATH);

            AddComponent<MoveActorComponent>().SetVelocity(70f);
            _inputMovement = AddComponent<InputMovementComponent>();
            AddComponent<CharacterAnimationComponent>().AddAnimation(FilePath.PLAYER_ATLAS_PATH);
            AddComponent<DebugActorComponent>();
        }

        public override void Update(float deltaTime)
        {
            Scene.Camera.Target = Position;
            base.Update(deltaTime);
        }

        public static void DisableInput() => _inputMovement.DisableInput();
        public static void EnableInput() => _inputMovement.EnableInput();
    }
}
