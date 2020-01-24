using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forecast.Core.Models;

namespace Forecast.DataAccess.Interfaces
{
    public interface IForecastRepository
    {
        Task InsertWeatherData(IEnumerable<City> payload);
        Task<IEnumerable<City>> GetCities();
        Task<IEnumerable<Weather>> GetWeatherData(int cityId, DateTime date);
    }
}
