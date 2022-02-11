using System.Collections.Generic;

namespace AirTek.Data.Deserializer
{
    public interface IDeserializer<T>
    {
        IEnumerable<T> Deserialize(string json);
    }
}
