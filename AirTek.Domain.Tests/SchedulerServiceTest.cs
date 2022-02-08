using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AirTek.Domain.Tests
{
    [TestClass]
    public class SchedulerServiceTest
    {
        private SchedulerService scheduler;

        [TestInitialize]
        public void TestInitialize()
        {
            scheduler = new SchedulerService();
        }

        [TestMethod]
        public void Schedule_WithOneOrder_WithNoFlights_OrderIsNotScheduled()
        {
            var name = "Some order name";
            var destination = "Some destination";
            scheduler.AddOrder(name, destination);

            scheduler.Schedule();

            var order = scheduler.Orders.First(order => order.Name == name);
            Assert.IsTrue(string.IsNullOrEmpty(order.FlightNumber));
        }

        [TestMethod]
        public void Schedule_WithNoOrders_WithOneFlight_FlightHasNoScheduledOrders()
        {
            var number = "1";
            var day = 1;
            var origin = "Some origin";
            var destination = "A different destination";
            scheduler.AddFlight(number, day, origin, destination);

            scheduler.Schedule();

            var flight = scheduler.Flights.First(flight => flight.Number == number);
            Assert.IsFalse(flight.Orders.Any());
        }

        [TestMethod]
        public void Schedule_WithOneOrder_WithNoFlightsMatchingDestination_OrderIsNotScheduled()
        {
            var orderName = "Some order name";
            var orderDestination = "Some destination";
            scheduler.AddOrder(orderName, orderDestination);

            var flightNumber = "1";
            var day = 1;
            var flightOrigin = "Some origin";
            var flightDestination = "A different destination";
            scheduler.AddFlight(flightNumber, day, flightOrigin, flightDestination);

            scheduler.Schedule();

            var order = scheduler.Orders.First(order => order.Name == orderName);
            Assert.IsTrue(string.IsNullOrEmpty(order.FlightNumber));
        }

        [TestMethod]
        public void Schedule_WithOneOrder_WithNoFlightsMatchingDestination_FlightHasNoOrdersScheduled()
        {
            var orderName = "Some order name";
            var orderDestination = "Some destination";
            scheduler.AddOrder(orderName, orderDestination);

            var flightNumber = "1";
            var day = 1;
            var flightOrigin = "Some origin";
            var flightDestination = "A different destination";
            scheduler.AddFlight(flightNumber, day, flightOrigin, flightDestination);

            scheduler.Schedule();

            var flight = scheduler.Flights.First(flight => flight.Number == flightNumber);
            Assert.IsFalse(flight.Orders.Any());
        }

        [TestMethod]
        public void Schedule_WithOneOrder_WithOneFlightMatchingDestination_OrderIsScheduledWithFlightNumber()
        {
            var orderName = "Some order name";
            var destination = "Some destination";
            scheduler.AddOrder(orderName, destination);

            var flightNumber = "1";
            var day = 1;
            var flightOrigin = "Some origin";
            scheduler.AddFlight(flightNumber, day, flightOrigin, destination);

            scheduler.Schedule();

            var order = scheduler.Orders.First(order => order.Name == orderName);
            Assert.AreEqual(flightNumber, order.FlightNumber);
        }

        [TestMethod]
        public void Schedule_WithOneOrder_WithOneFlightMatchingDestination_OrderIsScheduledWithFlightOrigin()
        {
            var orderName = "Some order name";
            var destination = "Some destination";
            scheduler.AddOrder(orderName, destination);

            var flightNumber = "1";
            var day = 1;
            var flightOrigin = "Some origin";
            scheduler.AddFlight(flightNumber, day, flightOrigin, destination);

            scheduler.Schedule();

            var order = scheduler.Orders.First(order => order.Name == orderName);
            Assert.AreEqual(flightOrigin, order.OriginCode);
        }

        [TestMethod]
        public void Schedule_WithOneOrder_WithOneFlightMatchingDestination_OrderIsScheduledWithFlightDay()
        {
            var orderName = "Some order name";
            var destination = "Some destination";
            scheduler.AddOrder(orderName, destination);

            var flightNumber = "1";
            var day = 1;
            var flightOrigin = "Some origin";
            scheduler.AddFlight(flightNumber, day, flightOrigin, destination);

            scheduler.Schedule();

            var order = scheduler.Orders.First(order => order.Name == orderName);
            Assert.AreEqual(day, order.Day);
        }

        [TestMethod]
        public void Schedule_WithOneOrder_WithOneFlightsMatchingDestinationHasReachedMaxCapacity_OrderIsNotScheduled()
        {
            var orderName = "Some order name";
            var destination = "Some destination";
            scheduler.AddOrder(orderName, destination);

            var flightNumber = "1";
            var day = 1;
            var flightOrigin = "Some origin";
            var maxCapacity = 0;
            scheduler.AddFlight(flightNumber, day, flightOrigin, destination, maxCapacity);

            scheduler.Schedule();

            var order = scheduler.Orders.First(order => order.Name == orderName);
            Assert.IsTrue(string.IsNullOrEmpty(order.FlightNumber));
        }
    }
}
