using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine.Attributes;

namespace Project
{
    [GameSettingsProperty("Game Debugger", "")]
    public class GameDebuggerSettings
    {
        [ShowEditor] public bool showInitialCutScene = true;
    }
}
