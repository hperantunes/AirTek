using AirTek.Data.Model;
using System.Collections.Generic;

namespace AirTek.Data.Mapper
{
    public class FlightEntryMapper : IEntryMapper<FlightEntry, KeyValuePair<string, FlightDetails>>
    {
        public FlightEntry Map(KeyValuePair<string, FlightDetails> flight)
        {
            var entry = new FlightEntry
            {
                Number = flight.Key,
                Details = flight.Value
            };
            return entry;
        }
    }
}
