namespace UmbrellaToolsKit.Storage
{
    public interface ISaveIntegration
    {
        T Get<T>(string filename);
        void Set<T>(T value);

        void Save(string filename);
    }
}
