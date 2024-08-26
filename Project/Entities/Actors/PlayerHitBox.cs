using UmbrellaToolsKit.Collision;
using Project.Components;
using UmbrellaToolsKit.EditorEngine;
using Microsoft.Xna.Framework;

namespace Project.Entities.Actors
{
    public class PlayerHitBox : Actor
    {
        private PlayerDebuggerSettings playerSettings;

        public override void Start()
        {
            HasGravity = false;

            playerSettings = GameSettingsProperty.GetProperty<PlayerDebuggerSettings>(FilePath.PLAYER_GAME_DEBUGGER_PATH);
            size = playerSettings.PlayerHitBoxAreaSizePoint;

            var constrain = AddComponent<PositionConstrainComponent>();
            constrain.SetTarget(Scene.Players[0]);
            constrain.SetOffset(playerSettings.PlayerOriginHitbox);
            
            if (playerSettings.ShowPlayerCollisionArea) AddComponent<DebugActorComponent>().SetColor(Color.Yellow);
        }

        public override void UpdateData(float deltaTime) {}
    }
}
