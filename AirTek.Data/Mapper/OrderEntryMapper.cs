using AirTek.Data.Model;
using AirTek.Domain;
using System.Collections.Generic;

namespace AirTek.Data.Mapper
{
    public class OrderEntryMapper : IEntryMapper<OrderEntry, KeyValuePair<string, OrderDetails>>
    {
        public OrderEntry Map(KeyValuePair<string, OrderDetails> order)
        {
            var entry = new OrderEntry
            {
                Name = order.Key,
                Details = order.Value
            };
            return entry;
        }
    }
}
