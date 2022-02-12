using System.Collections.Generic;

namespace AirTek.Data
{
    public interface IDataLoader<T>
    {
        IEnumerable<T> Load(string filePath);
    }
}
