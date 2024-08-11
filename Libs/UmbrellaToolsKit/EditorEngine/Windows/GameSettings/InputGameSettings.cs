using System.Collections.Generic;
using UmbrellaToolsKit.EditorEngine.Attributes;
using Microsoft.Xna.Framework.Input;

namespace UmbrellaToolsKit.EditorEngine.Windows.GameSettings
{

    [GameSettingsProperty("Input Settings", "games_settings/")]
    public class InputGameSettings
    {
        public struct InputData
        {
            public string InputName;
            public List<Keys> Keys;
        }

        public List<InputData> InputDataList;
    }
}
