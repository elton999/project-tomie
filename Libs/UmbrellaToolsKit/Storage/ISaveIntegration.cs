namespace UmbrellaToolsKit.Storage
{
    public interface ISaveIntegration
    {
        void Open(string filename);

        void Set<T>(T value);

        void Save(string filename);
    }
}
