namespace UmbrellaToolsKit.Storage
{
    public interface ISaveIntegration<T>
    {
        string Extension { get; }  

        T Get(string filename);
        void Set(object value);

        void Save(string filename);
    }
}
