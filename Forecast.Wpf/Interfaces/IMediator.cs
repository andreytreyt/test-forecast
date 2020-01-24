using System;

namespace Forecast.Wpf.Interfaces
{
    public interface IMediator
    {
        void Notify(object sender, EventArgs e);
    }
}
