using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Forecast.Core.Models;
using Forecast.DataAccess.Interfaces;
using Forecast.Wcf.Interfaces;
using NLog;

namespace Forecast.Wcf
{
    public class ForecastService : IForecastService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IForecastRepository repository;

        public ForecastService()
        {
            var container = AutofacConfig.ConfigureContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                this.repository = scope.Resolve<IForecastRepository>();
            }
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            logger.Info("Getting cities...");

            try
            {
                var cities = await repository.GetCities();
                return cities;
            }
            catch (Exception e)
            {
                logger.Error(e);
            }

            return null;
        }

        public async Task<IEnumerable<Weather>> GetWeather(int cityId, DateTime date)
        {
            logger.Info($"Getting wheater by cityId [{cityId}] and date [{date}]...");

            try
            {
                var weathers = await repository.GetWeatherData(cityId, date);
                return weathers;
            }
            catch (Exception e)
            {
                logger.Error(e);
            }

            return null;
        }
    }
}
