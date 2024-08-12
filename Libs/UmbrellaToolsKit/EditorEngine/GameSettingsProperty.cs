using System;
using System.Xml;

namespace UmbrellaToolsKit.EditorEngine
{
    public class GameSettingsProperty
    {
        public static GameSettingsProperty GetProperty(string pathFile, Type type = null)
        {
            GameSettingsProperty property = (GameSettingsProperty)Activator.CreateInstance(type == null ? typeof(GameSettingsProperty) : type);
            using (XmlReader reader = XmlReader.Create(pathFile))
            {
                try
                {
                    property = Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer.Deserialize<GameSettingsProperty>(reader, pathFile);
                } catch { };
                reader.Close();
            }
            return property;
        }
    }
}
