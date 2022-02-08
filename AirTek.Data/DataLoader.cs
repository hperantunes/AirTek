using System.Text.Json;

namespace AirTek.Data
{
    public abstract class DataLoader
    {
        public DataLoader(string filePath)
        {
            FilePath = filePath;
            SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public string FilePath { get; }
        public JsonSerializerOptions SerializerOptions { get; }
    }
}
