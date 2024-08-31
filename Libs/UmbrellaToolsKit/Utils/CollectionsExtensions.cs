using System.Collections.Generic;

namespace UmbrellaToolsKit.Utils
{
    public static class CollectionsExtensions
    {
        public static void AddForce<T>(this Dictionary<string, T> dictionary, string key, T value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
                return;
            }
            dictionary.Add(key, value);
        }
    }
}
