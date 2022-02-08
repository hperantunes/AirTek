using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AirTek.Data.Flights
{
    public class FlightsLoader : DataLoader
    {

        public FlightsLoader(string filePath) : base(filePath) { }

        public IEnumerable<FlightEntry> Load()
        {
            var jsonString = File.ReadAllText(FilePath);
            var data = JsonSerializer.Deserialize<IDictionary<string, FlightDetails>>(jsonString, SerializerOptions);

            var entries = data.AsEnumerable().Select(entry => new FlightEntry
            {
                Number = entry.Key,
                Details = entry.Value
            });

            return entries;
        }
    }
}
