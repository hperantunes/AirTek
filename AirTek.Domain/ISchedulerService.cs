using System.Collections.Generic;

namespace AirTek.Domain
{
    public interface ISchedulerService
    {
        IList<Order> Orders { get; }
        IList<Flight> Flights { get; }
        void AddOrder(string name, string destination);
        void AddFlight(string number, int day, string origin, string destination, int? maxCapacity = null);
        void Schedule();
    }
}
