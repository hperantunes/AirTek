using System.Collections.Generic;
using System.Linq;

namespace AirTek.Domain
{
    public class SchedulerService : ISchedulerService
    {
        public IList<Order> Orders { get; } = new List<Order>();
        public IList<Flight> Flights { get; } = new List<Flight>();

        public void Schedule()
        {
            var availableFlights = Flights.Where(flight => flight.HasCapacity);
            var unscheduledOrders = Orders.Where(order => string.IsNullOrEmpty(order.FlightNumber));

            foreach (var flight in availableFlights)
            {
                var orders = unscheduledOrders.Where(order => order.DestinationCode == flight.DestinationCode);
                foreach (var order in orders)
                {
                    if (!flight.HasCapacity)
                    {
                        break;
                    }

                    order.FlightNumber = flight.Number;
                    order.OriginCode = flight.OriginCode;
                    order.Day = flight.Day;

                    flight.Orders.Add(order);
                }
            }
        }
    }
}
