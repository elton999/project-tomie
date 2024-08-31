namespace UmbrellaToolsKit.Storage
{
    public interface ISaveIntegration<T>
    {
        T Get(string filename);
        void Set(T value);

        void Save(string filename);
    }
}
