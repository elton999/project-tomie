using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine.Attributes;

namespace Company.ClassLibrary1
{
    [GameSettingsProperty(nameof(PlayerDebuggerSettings), "/Content/")]
    public class PlayerDebuggerSettings
    {
        [ShowEditor] public bool ShowPlayerCollisionArea = false;
        [ShowEditor] public bool ShowPlayerHitBoxArea = false;
    }
}
