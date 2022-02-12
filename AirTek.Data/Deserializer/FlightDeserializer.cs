using AirTek.Data.Mapper;
using AirTek.Data.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace AirTek.Data.Deserializer
{
    public class FlightDeserializer : IDeserializer<FlightEntry>
    {
        private readonly IEntryMapper<FlightEntry, KeyValuePair<string, FlightDetails>> mapper;
        private readonly JsonSerializerOptions serializerOptions;

        public FlightDeserializer(IEntryMapper<FlightEntry, KeyValuePair<string, FlightDetails>> mapper)
        {
            this.mapper = mapper;
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public IEnumerable<FlightEntry> Deserialize(string json)
        {
            var data = JsonSerializer
                .Deserialize<IDictionary<string, FlightDetails>>(json, serializerOptions)
                .AsEnumerable();

            var entries = data.Select(mapper.Map);
            return entries;
        }
    }
}
