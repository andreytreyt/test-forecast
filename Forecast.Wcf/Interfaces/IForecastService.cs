using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Forecast.Core.Models;

namespace Forecast.Wcf.Interfaces
{
    [ServiceContract]
    public interface IForecastService
    {
        [OperationContract]
        Task<IEnumerable<City>> GetCities();
        [OperationContract]
        Task<IEnumerable<Weather>> GetWeather(int cityId, DateTime date);
    }
}
