using System.Collections.Generic;
using UmbrellaToolsKit.EditorEngine.Attributes;
using Microsoft.Xna.Framework.Input;

namespace UmbrellaToolsKit.EditorEngine.Windows.GameSettings
{
    [GameSettingsProperty("Input Settings", "games_settings/")]
    public class InputGameSettings : GameSettingsProperty
    {
        public struct InputData
        {
            public string InputName;
            public List<Keys> Keys;
        }

        [ShowEditor] public string Name;
        public List<InputData> InputDataList;
    }
}
