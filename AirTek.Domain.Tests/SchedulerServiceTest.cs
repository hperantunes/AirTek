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
            scheduler.Orders.Add(new Order 
            { 
                Name = name, 
                DestinationCode = destination 
            });

            scheduler.Schedule();

            var order = scheduler.Orders.First(order => order.Name == name);
            Assert.IsTrue(string.IsNullOrEmpty(order.FlightNumber));
        }

        [TestMethod]
        public void Schedule_WithNoOrders_WithOneFlight_FlightHasNoScheduledOrders()
        {
            var number = "Some flight number";
            var day = 1;
            var origin = "Some origin";
            var destination = "A different destination";
            scheduler.Flights.Add(new Flight
            {
                Number = number,
                Day = day,
                OriginCode = origin,
                DestinationCode = destination
            });

            scheduler.Schedule();

            var flight = scheduler.Flights.First(flight => flight.Number == number);
            Assert.IsFalse(flight.Orders.Any());
        }

        [TestMethod]
        public void Schedule_WithOneOrder_WithNoFlightsMatchingDestination_OrderIsNotScheduled()
        {
            var orderName = "Some order name";
            var orderDestination = "Some destination";
            scheduler.Orders.Add(new Order
            {
                Name = orderName,
                DestinationCode = orderDestination
            });

            var flightNumber = "Some flight number";
            var day = 1;
            var flightOrigin = "Some origin";
            var flightDestination = "A different destination";
            scheduler.Flights.Add(new Flight
            {
                Number = flightNumber,
                Day = day,
                OriginCode = flightOrigin,
                DestinationCode = flightDestination
            });

            scheduler.Schedule();

            var order = scheduler.Orders.First(order => order.Name == orderName);
            Assert.IsTrue(string.IsNullOrEmpty(order.FlightNumber));
        }

        [TestMethod]
        public void Schedule_WithOneOrder_WithNoFlightsMatchingDestination_FlightHasNoOrdersScheduled()
        {
            var orderName = "Some order name";
            var orderDestination = "Some destination";
            scheduler.Orders.Add(new Order
            {
                Name = orderName,
                DestinationCode = orderDestination
            });

            var flightNumber = "Some flight number";
            var day = 1;
            var flightOrigin = "Some origin";
            var flightDestination = "A different destination";
            scheduler.Flights.Add(new Flight
            {
                Number = flightNumber,
                Day = day,
                OriginCode = flightOrigin,
                DestinationCode = flightDestination
            });

            scheduler.Schedule();

            var flight = scheduler.Flights.First(flight => flight.Number == flightNumber);
            Assert.IsFalse(flight.Orders.Any());
        }

        [TestMethod]
        public void Schedule_WithOneOrder_WithOneFlightMatchingDestination_OrderIsScheduledWithFlightNumber()
        {
            var orderName = "Some order name";
            var destination = "Some destination";
            scheduler.Orders.Add(new Order
            {
                Name = orderName,
                DestinationCode = destination
            });

            var flightNumber = "Some flight number";
            var day = 1;
            var flightOrigin = "Some origin";
            scheduler.Flights.Add(new Flight
            {
                Number = flightNumber,
                Day = day,
                OriginCode = flightOrigin,
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
            scheduler.Orders.Add(new Order
            {
                Name = orderName,
                DestinationCode = destination
            });

            var flightNumber = "Some flight number";
            var day = 1;
            var flightOrigin = "Some origin";
            scheduler.Flights.Add(new Flight
            {
                Number = flightNumber,
                Day = day,
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
            scheduler.Orders.Add(new Order
            {
                Name = orderName,
                DestinationCode = destination
            });

            var flightNumber = "Some flight number";
            var day = 1;
            var flightOrigin = "Some origin";
            scheduler.Flights.Add(new Flight
            {
                Number = flightNumber,
                Day = day,
                OriginCode = flightOrigin,
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

            var flightNumber = "Some flight number";
            var day = 1;
            var flightOrigin = "Some origin";
            var maxCapacity = 0;
            scheduler.Flights.Add(new Flight
            {
                Number = flightNumber,
                Day = day,
                OriginCode = flightOrigin,
                DestinationCode = destination,
                MaxCapacity = 0
            });

            scheduler.Schedule();

            var order = scheduler.Orders.First(order => order.Name == orderName);
            Assert.IsTrue(string.IsNullOrEmpty(order.FlightNumber));
        }
    }
}
