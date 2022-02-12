using AirTek.Data;
using AirTek.Data.Deserializer;
using AirTek.Data.Mapper;
using AirTek.Data.Model;
using AirTek.Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

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
            services
                .AddTransient<ISchedulerService, SchedulerService>()
                .AddTransient<IEntryMapper<FlightEntry, KeyValuePair<string, FlightDetails>>, FlightEntryMapper>()
                .AddTransient<IEntryMapper<OrderEntry, KeyValuePair<string, OrderDetails>>, OrderEntryMapper>()
                .AddTransient<IDeserializer<OrderEntry>, OrderDeserializer>()
                .AddTransient<IDeserializer<FlightEntry>, FlightDeserializer>()
                .AddTransient<IDataLoader<OrderEntry>, DataLoader<OrderEntry>>()
                .AddTransient<IDataLoader<FlightEntry>, DataLoader<FlightEntry>>();
        }
    }
}
