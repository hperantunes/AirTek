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
            scheduler.Orders.Add(new Order 
            { 
                Name = name, 
                DestinationCode = "Some destination"
            });

            scheduler.Schedule();

            var order = scheduler.Orders.First(order => order.Name == name);
            Assert.IsTrue(string.IsNullOrEmpty(order.FlightNumber));
        }

        [TestMethod]
        public void Schedule_WithNoOrders_WithOneFlight_FlightHasNoScheduledOrders()
        {
            var number = "Some flight number";
            scheduler.Flights.Add(new Flight
            {
                Number = number,
                Day = 1,
                OriginCode = "Some origin",
                DestinationCode = "Some destination"
            });

            scheduler.Schedule();

            var flight = scheduler.Flights.First(flight => flight.Number == number);
            Assert.IsFalse(flight.Orders.Any());
        }

        [TestMethod]
        public void Schedule_WithOneOrder_WithNoFlightsMatchingDestination_OrderIsNotScheduled()
        {
            var orderName = "Some order name";
            scheduler.Orders.Add(new Order
            {
                Name = orderName,
                DestinationCode = "Some destination"
            });
            scheduler.Flights.Add(new Flight
            {
                Number = "Some flight number",
                Day = 1,
                OriginCode = "Some origin",
                DestinationCode = "A different destination"
            });

            scheduler.Schedule();

            var order = scheduler.Orders.First(order => order.Name == orderName);
            Assert.IsTrue(string.IsNullOrEmpty(order.FlightNumber));
        }

        [TestMethod]
        public void Schedule_WithOneOrder_WithNoFlightsMatchingDestination_FlightHasNoOrdersScheduled()
        {
            var flightNumber = "Some flight number";
            scheduler.Orders.Add(new Order
            {
                Name = "Some order name",
                DestinationCode = "Some destination"
            });
            scheduler.Flights.Add(new Flight
            {
                Number = flightNumber,
                Day = 1,
                OriginCode = "Some origin",
                DestinationCode = "A different destination"
            });

            scheduler.Schedule();

            var flight = scheduler.Flights.First(flight => flight.Number == flightNumber);
            Assert.IsFalse(flight.Orders.Any());
        }

        [TestMethod]
        public void Schedule_WithOneOrder_WithOneFlightMatchingDestination_OrderIsScheduledWithFlightNumber()
        {
            var orderName = "Some order name";
            var flightNumber = "Some flight number";
            var destination = "Some destination";
            scheduler.Orders.Add(new Order
            {
                Name = orderName,
                DestinationCode = destination
            });
            scheduler.Flights.Add(new Flight
            {
                Number = flightNumber,
                Day = 1,
                OriginCode = "Some origin",
                DestinationCode = destination
            });

            scheduler.Schedule();

            var order = scheduler.Orders.First(order => order.Name == orderName);
            Assert.AreEqual(flightNumber, order.FlightNumber);
        }

        [TestMethod]
        public void Schedule_WithOneOrder_WithOneFlightMatchingDestination_OrderIsScheduledWithFlightOrigin()
        {
            var orderName = "Some order name";
            var destination = "Some destination";
            var flightOrigin = "Some origin";
            scheduler.Orders.Add(new Order
            {
                Name = orderName,
                DestinationCode = destination
            });
            scheduler.Flights.Add(new Flight
            {
                Number = "Some flight number",
                Day = 1,
                OriginCode = flightOrigin,
                DestinationCode = destination
            });

            scheduler.Schedule();

            var order = scheduler.Orders.First(order => order.Name == orderName);
            Assert.AreEqual(flightOrigin, order.OriginCode);
        }

        [TestMethod]
        public void Schedule_WithOneOrder_WithOneFlightMatchingDestination_OrderIsScheduledWithFlightDay()
        {
            var orderName = "Some order name";
            var destination = "Some destination";
            var day = 1;
            scheduler.Orders.Add(new Order
            {
                Name = orderName,
                DestinationCode = destination
            });
            scheduler.Flights.Add(new Flight
            {
                Number = "Some flight number",
                Day = day,
                OriginCode = "Some origin",
                DestinationCode = destination
            });

            scheduler.Schedule();

            var order = scheduler.Orders.First(order => order.Name == orderName);
            Assert.AreEqual(day, order.Day);
        }

        [TestMethod]
        public void Schedule_WithOneOrder_WithOneFlightsMatchingDestinationHasReachedMaxCapacity_OrderIsNotScheduled()
        {
            var orderName = "Some order name";
            var destination = "Some destination";
            scheduler.Orders.Add(new Order
            {
                Name = orderName,
                DestinationCode = destination
            });
            scheduler.Flights.Add(new Flight
            {
                Number = "Some flight number",
                Day = 1,
                OriginCode = "Some origin",
                DestinationCode = destination,
                MaxCapacity = 0
            });

            scheduler.Schedule();

            var order = scheduler.Orders.First(order => order.Name == orderName);
            Assert.IsTrue(string.IsNullOrEmpty(order.FlightNumber));
        }
    }
}
