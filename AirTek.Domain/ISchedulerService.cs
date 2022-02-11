using System.Collections.Generic;

namespace AirTek.Domain
{
    public interface ISchedulerService
    {
        IList<Order> Orders { get; }
        IList<Flight> Flights { get; }
        void Schedule();
    }
}
