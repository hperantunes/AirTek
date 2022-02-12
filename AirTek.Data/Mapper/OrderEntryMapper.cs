using AirTek.Data.Model;
using System.Collections.Generic;

namespace AirTek.Data.Mapper
{
    public class OrderEntryMapper : IEntryMapper<OrderEntry, KeyValuePair<string, OrderDetails>>
    {
        public OrderEntry Map(KeyValuePair<string, OrderDetails> flight)
        {
            var entry = new OrderEntry
            {
                Name = flight.Key,
                Details = flight.Value
            };
            return entry;
        }
    }
}
