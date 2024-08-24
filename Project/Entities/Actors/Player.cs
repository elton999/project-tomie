using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using Project.Components;
using Microsoft.Xna.Framework.Graphics;
using Company.ClassLibrary1;
using UmbrellaToolsKit.EditorEngine;

namespace Project.Entities.Actors
{
    public class Player : Actor
    {
        private static InputMovementComponent _inputMovement;
        private PlayerDebuggerSettings playerSettings;

        public override void Start()
        {
            playerSettings = GameSettingsProperty.GetProperty<PlayerDebuggerSettings>(FilePath.PLAYER_GAME_DEBUGGER_PATH);

            size = playerSettings.PlayerSizeCollisionAreaPoint;
            Origin = playerSettings.PlayerOriginSprite;

            HasGravity = false;

            tag = "player";
            base.Start();


            Sprite = Scene.Content.Load<Texture2D>(FilePath.PLAYER_SPRITE_PATH);

            AddComponent<MoveActorComponent>().SetVelocity(playerSettings.PlayerSpeed);
            _inputMovement = AddComponent<InputMovementComponent>();
            AddComponent<CharacterAnimationComponent>().AddAnimation(FilePath.PLAYER_ATLAS_PATH);

            if (playerSettings.ShowPlayerCollisionArea) AddComponent<DebugActorComponent>();
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
