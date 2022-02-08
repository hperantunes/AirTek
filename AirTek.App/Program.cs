using AirTek.Data.Flights;
using AirTek.Data.Orders;
using AirTek.Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace AirTek.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            services
                .AddSingleton<IStartableApplication, AirTekApplication>()
                .BuildServiceProvider()
                .GetService<IStartableApplication>()
                .Start();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var ordersFilePath = ConfigurationManager.AppSettings.Get("OrdersFilePath");
            var flightsFilePath = ConfigurationManager.AppSettings.Get("FlightsFilePath");

            services
                .AddTransient<ISchedulerService, SchedulerService>()
                .AddTransient(s => new OrdersLoader(ordersFilePath))
                .AddTransient(s => new FlightsLoader(flightsFilePath));
        }
    }
}
