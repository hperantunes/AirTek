using AirTek.Data.Mapper;
using AirTek.Data.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace AirTek.Data.Deserializer
{
    public class OrderDeserializer : IDeserializer<OrderEntry>
    {
        private readonly IEntryMapper<OrderEntry, KeyValuePair<string, OrderDetails>> mapper;
        private readonly JsonSerializerOptions serializerOptions;

        public OrderDeserializer(IEntryMapper<OrderEntry, KeyValuePair<string, OrderDetails>> mapper)
        {
            this.mapper = mapper;
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public IEnumerable<OrderEntry> Deserialize(string json)
        {
            var data = JsonSerializer.Deserialize<IDictionary<string, OrderDetails>>(json, serializerOptions);
            var entries = data.AsEnumerable().Select(mapper.Map);
            return entries;
        }
    }
}
