using System.Collections.Generic;
using UmbrellaToolsKit.EditorEngine.Attributes;
using Microsoft.Xna.Framework.Input;

namespace UmbrellaToolsKit.EditorEngine.Windows.GameSettings
{
    [GameSettingsProperty("Input Settings", "games_settings/")]
    public class InputGameSettings : GameSettingsProperty
    {
        [System.Serializable]
        public struct InputData
        {
            [ShowEditor] public string InputName;
            [ShowEditor] public List<Keys> Keys;
        }

        [ShowEditor] public string Name;
        [ShowEditor] public List<InputData> InputDataList;
    }
}
