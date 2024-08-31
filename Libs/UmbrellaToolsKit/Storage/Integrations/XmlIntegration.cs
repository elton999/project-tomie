using System;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;

namespace UmbrellaToolsKit.Storage.Integrations
{
    public abstract class XmlIntegration<T> : ISaveIntegration<T>
    {
        private T _values;

        public T Get(string filename)
        {
            T values = (T)Activator.CreateInstance(typeof(T));
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    values = IntermediateSerializer.Deserialize<T>(reader, filename);
                    reader.Close();
                }
            }
            catch { };

            return values;
        }

        public void Save(string filename)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(filename, settings))
            {
                IntermediateSerializer.Serialize(writer, _values, null);
                writer.Close();
            }
        }

        public void Set(T value) => _values = value;
    }
}
