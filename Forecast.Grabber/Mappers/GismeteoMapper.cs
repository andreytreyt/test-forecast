using AutoMapper;
using Forecast.Core.Models;
using Forecast.Grabber.Interfaces;
using Forecast.Grabber.Models;

namespace Forecast.Grabber.Mappers
{
    internal class GismeteoMapper : IWeatherMapper<GismeteoDataModel>
    {
        private readonly IMapper mapper;

        public GismeteoMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<GismeteoDataModel, City>()
                .ForMember(x => x.Weathers, opt => opt.MapFrom<GismeteoResolver>())
                .ForMember(x => x.Name, opt => opt.MapFrom(m => m.City)));

            mapper = config.CreateMapper();
        }
        public City Map(GismeteoDataModel data)
        {
            var result = mapper.Map<City>(data);
            return result;
        }
    }
}
