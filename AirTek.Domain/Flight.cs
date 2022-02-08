using System.Collections.Generic;

namespace AirTek.Domain
{
    public class Flight
    {
        public string Number { get; set; }
        public string OriginCode { get; set; }
        public string DestinationCode { get; set; }
        public int Day { get; set; }
        public IList<Order> Orders { get; set; } = new List<Order>();
        public int MaxCapacity { get; set; } = 20;
        public bool HasCapacity => Orders.Count < MaxCapacity;
    }
}
