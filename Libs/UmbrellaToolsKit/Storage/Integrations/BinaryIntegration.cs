using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace UmbrellaToolsKit.Storage.Integrations
{
    public class BinaryIntegration<T> : ISaveIntegration<T>
    {
        object _values;

        public T Get(string filename)
        {
            T values = (T)Activator.CreateInstance(typeof(T));
            try
            {
                using (BinaryReader binaryReader = new BinaryReader(new FileStream(filename, FileMode.Open)))
                {
                    string base64String = binaryReader.ReadString();
                    byte[] bytes = Convert.FromBase64String(base64String);
                    string jsonString = Encoding.UTF8.GetString(bytes);

                    values = (T)JsonConvert.DeserializeObject(jsonString);
                    Console.WriteLine(JsonConvert.SerializeObject(values));
                    binaryReader.Close();
                }

            }
            catch { };

            return values;
        }

        public void Save(string filename)
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(new FileStream(filename, FileMode.Create)))
            {
                string jsonString = JsonConvert.SerializeObject(_values);
                byte[] bytes = Encoding.UTF8.GetBytes(jsonString);

                binaryWriter.Write(Convert.ToBase64String(bytes));
                binaryWriter.Close();
            }
        }

        public void Set(object value) => _values = value;
    }
}
