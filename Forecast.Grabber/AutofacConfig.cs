using Autofac;
using Forecast.DataAccess;
using Forecast.DataAccess.Interfaces;
using Forecast.Grabber.Interfaces;
using Forecast.Grabber.Services;

namespace Forecast.Grabber
{
    public class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<GistemeoGrabService>().As<IGrabService>();
            builder.RegisterType<ForecastRepository>().As<IForecastRepository>();
            return builder.Build();
        }
    }
}
