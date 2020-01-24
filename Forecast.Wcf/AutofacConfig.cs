using Autofac;
using Forecast.DataAccess;
using Forecast.DataAccess.Interfaces;

namespace Forecast.Wcf
{
    public class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ForecastRepository>().As<IForecastRepository>();
            return builder.Build();
        }
    }
}
