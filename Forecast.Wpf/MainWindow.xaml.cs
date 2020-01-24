using System;
using System.Linq;
using System.Windows;
using Forecast.Wpf.ForecastService;
using Forecast.Wpf.Interfaces;

namespace Forecast.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly IMediator mediator;
        private readonly ForecastServiceClient client;

        public MainWindow()
        {
            InitializeComponent();

            mediator = new ForecastMediator(CityBox, DatePicker, ForecastButton);

            client = new ForecastServiceClient();
            var cities = client.GetCities();

            CityBox.DataContext = cities;
        }

        private void Control_OnStateChanged(object sender, EventArgs e)
        {
            mediator.Notify(sender, e);
        }

        private void ForecastButton_OnClick(object sender, RoutedEventArgs e)
        {
            var cityId = CityBox.SelectedValue;
            var date = DatePicker.SelectedDate??DateTime.Now;

            var weather = client.GetWeather(Convert.ToInt32(cityId), date);

            if (!weather.Any())
            {
                MessageBox.Show($"Forecast has no data for {date.ToShortDateString()}. Please select other date.");
                return;
            }

            WeatherGrid.DataContext = weather.Select(x => new
            {
                x.Temperature,
                Type = x.Type.Name
            });
            WeatherGrid.Visibility = Visibility.Visible;
        }
    }
}
