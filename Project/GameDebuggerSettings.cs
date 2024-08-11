using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine.Attributes;
using UmbrellaToolsKit.EditorEngine;

namespace Project
{
    [GameSettingsProperty("GameDebuggerSettings", "/Content/")]
    public class GameDebuggerSettings : GameSettingsProperty
    {
        [ShowEditor] public bool showInitialCutScene = true;
    }
}
