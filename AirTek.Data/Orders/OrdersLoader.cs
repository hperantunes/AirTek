using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AirTek.Data.Orders
{
    public class OrdersLoader : DataLoader
    {
        public OrdersLoader(string filePath) : base(filePath) { }

        public IEnumerable<OrderEntry> Load()
        {
            var jsonString = File.ReadAllText(FilePath);
            var data = JsonSerializer.Deserialize<IDictionary<string, OrderDetails>>(jsonString, SerializerOptions);

            var entries = data.AsEnumerable().Select(entry => new OrderEntry
            {
                Name = entry.Key,
                Details = entry.Value
            });

            return entries;
        }
    }
}
