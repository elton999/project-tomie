using System.Xml;

namespace UmbrellaToolsKit.EditorEngine
{
    public class GameSettingsProperty
    {
        public static GameSettingsProperty GetProperty(string pathFile)
        {
            GameSettingsProperty property = null;
            using (XmlReader reader = XmlReader.Create(pathFile))
            {
                property = Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer.Deserialize<GameSettingsProperty>(reader, pathFile);
                reader.Close();
            }
            return property;
        }
    }
}
