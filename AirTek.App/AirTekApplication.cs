using AirTek.Data;
using AirTek.Data.Model;
using AirTek.Domain;
using System;
using System.Configuration;
using System.Linq;

namespace AirTek.App
{
    internal class AirTekApplication : IStartableApplication
    {
        private readonly string ordersFilePath;
        private readonly string flightsFilePath;

        private readonly ISchedulerService scheduler;
        private readonly IDataLoader<OrderEntry> ordersLoader;
        private readonly IDataLoader<FlightEntry> flightsLoader;

        public AirTekApplication(ISchedulerService scheduler, IDataLoader<OrderEntry> ordersLoader, IDataLoader<FlightEntry> flightsLoader)
        {
            ordersFilePath = ConfigurationManager.AppSettings.Get("OrdersFilePath");
            flightsFilePath = ConfigurationManager.AppSettings.Get("FlightsFilePath");

            this.scheduler = scheduler;
            this.ordersLoader = ordersLoader;
            this.flightsLoader = flightsLoader;
        }

        private void LoadFlights()
        {
            var flights = flightsLoader.Load(flightsFilePath);

            foreach (var flight in flights)
            {
                var details = flight.Details;
                scheduler.Flights.Add(new Flight
                {
                    Number = flight.Number,
                    Day = details.Day,
                    OriginCode = details.Origin,
                    DestinationCode = details.Destination
                });
            }
        }

        private void LoadOrders()
        {
            var orders = ordersLoader.Load(ordersFilePath);

            foreach (var order in orders)
            {
                var priority = order.Details.Service == "same-day"
                    ? OrderPriority.SameDay
                    : order.Details.Service == "next-day"
                        ? OrderPriority.NextDay
                        : OrderPriority.Regular;

                scheduler.Orders.Add(new Order
                {
                    Name = order.Name,
                    DestinationCode = order.Details.Destination,
                    OrderPriority = priority
                });
            }
        }

        private void ScheduleOrders()
        {
            scheduler.Schedule();
        }

        private static string RenderFlightDisplayText(Flight flight)
        {
            return $"Flight: {flight.Number}, departure: {flight.OriginCode}, arrival: {flight.DestinationCode}, day: {flight.Day}";
        }

        private static string RenderOrderDisplayText(Order order)
        {
            if (string.IsNullOrEmpty(order.FlightNumber))
            {
                return $"Order: {order.Name}, flightNumber: not scheduled";
            }

            return $"Order: {order.Name}, flightNumber: {order.FlightNumber}, departure: {order.OriginCode}, arrival: {order.DestinationCode}, day: {order.Day}, priority: {order.OrderPriority}";
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
