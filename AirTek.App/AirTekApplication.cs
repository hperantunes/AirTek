using AirTek.Data.Flights;
using AirTek.Data.Orders;
using AirTek.Domain;
using System;
using System.Linq;

namespace AirTek.App
{
    internal class AirTekApplication : IStartableApplication
    {
        private readonly ISchedulerService scheduler;
        private readonly OrdersLoader ordersLoader;
        private readonly FlightsLoader flightsLoader;

        public AirTekApplication(ISchedulerService scheduler, OrdersLoader ordersLoader, FlightsLoader flightsLoader)
        {
            this.scheduler = scheduler;
            this.ordersLoader = ordersLoader;
            this.flightsLoader = flightsLoader;
        }

        private void LoadFlights()
        {
            var flights = flightsLoader.Load();

            foreach (var flight in flights)
            {
                var details = flight.Details;
                scheduler.AddFlight(flight.Number, details.Day, details.Origin, details.Destination);
            }
        }

        private void LoadOrders()
        {
            var orders = ordersLoader.Load();

            foreach (var order in orders)
            {
                var details = order.Details;
                scheduler.AddOrder(order.Name, details.Destination);
            }
        }

        private void ScheduleOrders()
        {
            scheduler.Schedule();
        }

        private static string RenderFlightDisplayText(Flight flight)
        {
            var message = $"Flight: {flight.Number}, departure: {flight.OriginCode}, arrival: {flight.DestinationCode}, day: {flight.Day}";
            return message;
        }

        private static string RenderOrderDisplayText(Order order)
        {
            if (string.IsNullOrEmpty(order.FlightNumber))
            {
                return $"Order: {order.Name}, flightNumber: not scheduled";
            }

            var message = $"Order: {order.Name}, flightNumber: {order.FlightNumber}, departure: {order.OriginCode}, arrival: {order.DestinationCode}, day: {order.Day}";
            return message;
        }

        private void RenderFlights()
        {
            var flights = scheduler.Flights.Select(RenderFlightDisplayText);

            Console.WriteLine();

            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
            }

            Console.WriteLine();
        }

        private void RenderOrders()
        {
            var orders = scheduler.Orders.Select(RenderOrderDisplayText);

            Console.WriteLine();

            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }

            Console.WriteLine();
        }

        public void Start()
        {
            Console.WriteLine("Welcome to Transport.ly!");
            Console.WriteLine("------------------------");
            Console.WriteLine();

            Console.WriteLine("Press ENTER to load flights.");
            Console.ReadLine();

            LoadFlights();

            Console.WriteLine("Flights loaded successfully!");
            Console.WriteLine();

            Console.WriteLine("Press ENTER to list loaded flights.");
            Console.ReadLine();

            RenderFlights();

            Console.WriteLine("Press ENTER to load and schedule orders.");
            Console.ReadLine();

            LoadOrders();
            ScheduleOrders();

            Console.WriteLine("Orders loaded and scheduled successfully!");
            Console.WriteLine();

            Console.WriteLine("Press ENTER to list scheduled orders.");
            Console.ReadLine();

            RenderOrders();

            Console.WriteLine("Press ENTER to exit the application.");
            Console.WriteLine();
            Console.ReadLine();

            Console.WriteLine("Goodbye!");
        }
    }
}
