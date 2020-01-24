using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Forecast.Core.Models;
using Forecast.DataAccess.Interfaces;
using Forecast.Wcf.Interfaces;

namespace Forecast.Wcf
{
    public class ForecastService : IForecastService
    {
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
            var cities = await repository.GetCities();
            return cities;
        }

        public async Task<IEnumerable<Weather>> GetWeather(int cityId, DateTime date)
        {
            var weathers = await repository.GetWeatherData(cityId, date);
            return weathers;
        }
    }
}
