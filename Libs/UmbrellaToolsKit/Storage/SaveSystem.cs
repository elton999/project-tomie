using System;

namespace UmbrellaToolsKit.Storage
{
    public class SaveSystem : IDisposable
    {
        private SaveSettings _integration;

        public SaveSystem(SaveSettings integration) => _integration = integration;

        public void SetString(string key, string value)
        {
        }

        public string GetString(string key, string defaultValue)
        {
            return defaultValue;
        }

        public void SetInt(string key, int value)
        {

        }

        public int GetInt(string key, int defaultValue)
        {
            return defaultValue;
        }

        public void SetFloat(string key, float value)
        {

        }

        public float GetFloat(string key, float defaultValue)
        {
            return defaultValue;
        }

        public void Save(string filename) => _integration.SaveIntegration.Save(filename);

        public void Save() => Save(_integration.FilePath);

        public void Dispose() { }
    }
}
