using System.Collections.Generic;
using System.Threading.Tasks;
using Forecast.Core.Models;

namespace Forecast.Grabber.Interfaces
{
    internal interface IGrabService
    {
        Task<List<City>> Grab();
    }
}
