using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using Forecast.DataAccess.Interfaces;
using Forecast.Grabber.Interfaces;

namespace Forecast.Grabber
{
    class Program
    {
        private static IContainer container;

        static async Task Main(string[] args)
        {
            container = AutofacConfig.ConfigureContainer();

            try
            {
                await Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadKey();
        }

        static async Task Execute()
        {
            IGrabService grabService;
            IForecastRepository repository;

            using (var scope = container.BeginLifetimeScope())
            {
                grabService = scope.Resolve<IGrabService>();
                repository = scope.Resolve<IForecastRepository>();
            }

            var sw = new Stopwatch();

            Console.WriteLine("Start grabbing...");
            sw.Start();
            var payload = await grabService.Grab();
            sw.Stop();
            Console.WriteLine($"Grabbed items: {payload.Count}");
            Console.WriteLine($"Grabbing time: {sw.Elapsed}");
            sw.Reset();

            Console.WriteLine("Start inserting...");
            sw.Start();
            await repository.InsertWeatherData(payload);
            sw.Stop();
            Console.WriteLine($"Inserting time: {sw.Elapsed}");
        }
    }
}