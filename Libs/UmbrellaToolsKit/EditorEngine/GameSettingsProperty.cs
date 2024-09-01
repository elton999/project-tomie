using System;
using UmbrellaToolsKit.Storage;
using UmbrellaToolsKit.Storage.Integrations;

namespace UmbrellaToolsKit.EditorEngine
{
    public class GameSettingsProperty
    {
        public static ISaveIntegration<GameSettingsProperty> SaveIntegration = new XmlIntegration<GameSettingsProperty>();

        public static GameSettingsProperty GetProperty(string pathFile)
        {
            var timer = new Utils.Timer();

            timer.Begin();
            GameSettingsProperty property = SaveIntegration.Get(pathFile);
            timer.End();

            Log.Write($"[{nameof(GameSettingsProperty)}] reading: {pathFile}, timer: {timer.GetTotalSeconds()}");
            return property;
        }

        public static GameSettingsProperty GetGameSettingsProperty(string pathFile) => GetProperty(pathFile);

        public static T GetProperty<T>(string pathFile) where T : GameSettingsProperty
        {
            var property = GetProperty(pathFile);

            if (property is T) return (T)property;
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static object GetProperty(string pathFile, Type type)
        {
            var backUp = Activator.CreateInstance(type);
            var property = (object)GetProperty(pathFile);

            if (property.GetType() == type) return property;

            return backUp;
        }
    }
}
