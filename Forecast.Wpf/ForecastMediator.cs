using System;
using System.Windows.Controls;
using Forecast.Wpf.Interfaces;

namespace Forecast.Wpf
{
    public class ForecastMediator : IMediator
    {
        private readonly ComboBox cityBox;
        private readonly DatePicker datePicker;
        private readonly Button forecastButton;

        public ForecastMediator(
            ComboBox cityBox, 
            DatePicker datePicker,
            Button forecastButton)
        {
            this.cityBox = cityBox;
            this.datePicker = datePicker;
            this.forecastButton = forecastButton;
        }

        public void Notify(object sender, EventArgs e)
        {
            forecastButton.IsEnabled = cityBox.SelectedIndex > -1 && 
                                       datePicker.SelectedDate.HasValue;
        }
    }
}
