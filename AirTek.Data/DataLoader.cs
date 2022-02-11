using AirTek.Data.Deserializer;
using System.Collections.Generic;
using System.IO;

namespace AirTek.Data
{
    public class DataLoader<T> : IDataLoader<T>
    {
        private readonly IDeserializer<T> deserializer;

        public DataLoader(IDeserializer<T> deserializer)
        {
            this.deserializer = deserializer;
        }

        public IEnumerable<T> Load(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var data = deserializer.Deserialize(json);
            return data;
        }
    }
}
