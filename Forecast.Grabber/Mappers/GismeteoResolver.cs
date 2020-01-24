using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Forecast.Core.Models;
using Forecast.Grabber.Models;

namespace Forecast.Grabber.Mappers
{
    internal class GismeteoResolver : IValueResolver<GismeteoDataModel, City, List<Weather>>
    {
        public List<Weather> Resolve(GismeteoDataModel source, City destination, List<Weather> destMember, ResolutionContext context)
        {
            var date1970 = new DateTime(1970, 1, 1);

            return source.Data.Select(x => new Weather
            {
                Date = date1970.AddMilliseconds(Convert.ToDouble(x[0])),
                Temperature = Convert.ToDouble(x[1]),
                Type = new WeatherType
                {
                    Name = x[2].ToString()
                }
            }).ToList();
        }
    }
}
