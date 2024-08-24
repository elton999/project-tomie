using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.EditorEngine.Attributes;

namespace Company.ClassLibrary1
{
    [GameSettingsProperty(nameof(PlayerDebuggerSettings), "/Content/")]
    public class PlayerDebuggerSettings : GameSettingsProperty
    {
        [ShowEditor] public bool ShowPlayerCollisionArea = false;
        [ShowEditor] public bool ShowPlayerHitBoxArea = false;
        [ShowEditor] public Vector2 PlayerSizeCollisionArea = new Vector2(20, 20);
        public Point PlayerSizeCollisionAreaPoint => PlayerSizeCollisionArea.ToPoint();
        [ShowEditor] public Vector2 PlayerOriginSprite = new Vector2(20, 46);
        [ShowEditor] public float PlayerSpeed = 70f;
    }
}
