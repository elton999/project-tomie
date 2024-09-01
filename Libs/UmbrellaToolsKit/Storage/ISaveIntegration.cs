namespace UmbrellaToolsKit.Storage
{
    public interface ISaveIntegration<T>
    {
        T Get(string filename);
        void Set(object value);

        void Save(string filename);
    }
}
