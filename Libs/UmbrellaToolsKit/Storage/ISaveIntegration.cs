using System;

namespace UmbrellaToolsKit.Storage
{
    public interface ISaveIntegration<T>
    {
        string Extension { get; }  

        object Get(string filename, Type type = null);
        void Set(object value);

        void Save(string filename);
    }
}
