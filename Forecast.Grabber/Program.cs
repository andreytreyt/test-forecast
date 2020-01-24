using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using Forecast.DataAccess.Interfaces;
using Forecast.Grabber.Interfaces;
using NLog;

namespace Forecast.Grabber
{
    class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static IContainer container;

        static async Task Main(string[] args)
        {
            container = AutofacConfig.ConfigureContainer();

            try
            {
                logger.Info("Start of grabber execution...");
                await Execute();
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
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

            logger.Info("Start grabbing...");
            sw.Start();
            var payload = await grabService.Grab();
            sw.Stop();
            logger.Info($"Grabbed items: {payload.Count}");
            logger.Info($"Grabbing time: {sw.Elapsed}");
            sw.Reset();

            logger.Info("Start inserting...");
            sw.Start();
            await repository.InsertWeatherData(payload);
            sw.Stop();
            logger.Info($"Inserting time: {sw.Elapsed}");
        }
    }
}