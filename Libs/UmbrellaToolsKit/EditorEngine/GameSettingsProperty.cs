using System;
using System.Xml;

namespace UmbrellaToolsKit.EditorEngine
{
    public class GameSettingsProperty
    {
        public static GameSettingsProperty GetProperty(string pathFile, Type type = null)
        {
            var timer = new Utils.Timer();
            timer.Begin();

            GameSettingsProperty property = (GameSettingsProperty)Activator.CreateInstance(type == null ? typeof(GameSettingsProperty) : type);
            using (XmlReader reader = XmlReader.Create(pathFile))
            {
                try
                {
                    property = Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer.Deserialize<GameSettingsProperty>(reader, pathFile);
                }
                catch { };
                reader.Close();
                timer.End();
                Log.Write($"[{nameof(GameSettingsProperty)}] reading: {pathFile}");
            }

            timer.End();
            Log.Write($"[{nameof(GameSettingsProperty)}] reading: {pathFile}, timer: {timer.GetTotalSeconds()}");
            return property;
        }
    }
}
