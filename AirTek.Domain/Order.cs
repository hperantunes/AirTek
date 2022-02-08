namespace AirTek.Domain
{
    public class Order
    {
        public string Name { get; set; }
        public string FlightNumber { get; set; }
        public string OriginCode { get; set; }
        public string DestinationCode { get; set; }
        public int Day { get; set; }
    }
}
