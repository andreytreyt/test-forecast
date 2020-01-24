using Forecast.Core.Models;

namespace Forecast.Grabber.Interfaces
{
    internal interface IWeatherMapper<T>
    {
        City Map(T data);
    }
}
