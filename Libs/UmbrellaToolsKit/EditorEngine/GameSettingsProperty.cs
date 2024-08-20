using System;
using System.Xml;

namespace UmbrellaToolsKit.EditorEngine
{
    public class GameSettingsProperty
    {
        public static GameSettingsProperty GetProperty(string pathFile, Type type)
        {
            var timer = new Utils.Timer();
            timer.Begin();

            GameSettingsProperty property = (GameSettingsProperty)Activator.CreateInstance(type);
            try
            {
                using (XmlReader reader = XmlReader.Create(pathFile))
                {
                    property = Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer.Deserialize<GameSettingsProperty>(reader, pathFile);
                    reader.Close();
                }
            }
            catch
            { };

            timer.End();
            Log.Write($"[{nameof(GameSettingsProperty)}] reading: {pathFile}, timer: {timer.GetTotalSeconds()}");
            return property;
        }

        public static GameSettingsProperty GetGameSettingsProperty(string pathFile)
        {
            return GetProperty(pathFile, typeof(GameSettingsProperty));
        }

        public static T GetProperty<T>(string pathFile) where T : GameSettingsProperty
        {
            return (T)GetProperty(pathFile, typeof(T));
        }
    }
}
