using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Forecast.Core.Models;
using Forecast.Grabber.Interfaces;
using Forecast.Grabber.Mappers;
using Forecast.Grabber.Models;
using Newtonsoft.Json;

namespace Forecast.Grabber.Services
{
    internal class GistemeoGrabService : IGrabService
    {
        private readonly string url;

        public GistemeoGrabService()
        {
            url = ConfigurationManager.AppSettings["GismeteoUrl"];
        }

        public async Task<List<City>> Grab()
        {
            var result = new List<City>();

            const string tenDaysUrlPath = "10-days/";
            const string mgMediaData = "MG.Media.data = ";

            var cityFrameRegex = new Regex("<!-- City frame -->(.|\n)*<!-- End City frame -->");
            var cityRegex = new Regex("/weather-\\S*/");
            var weatherRegex = new Regex(mgMediaData + "{.*}");
            
            var gismeteoMapper = new GismeteoMapper();
            var httpClient = new HttpClient { BaseAddress = new Uri(url) };

            var cityRes = await httpClient.GetStringAsync(string.Empty);
            var cityFrameMatch = cityFrameRegex.Match(cityRes);
            var cityUrlPathes = cityRegex.Matches(cityFrameMatch.Value)
                .Cast<Match>()
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            var tasks = cityUrlPathes.Select(async cityUrlPath =>
            {
                var urlPath = $"{cityUrlPath}{tenDaysUrlPath}";
                var weatherRes = await httpClient.GetStringAsync(urlPath);
                var weatherMatch = weatherRegex.Match(weatherRes);
                var dataJson = weatherMatch.Value.Remove(0, mgMediaData.Length);
                var data = JsonConvert.DeserializeObject<GismeteoDataModel>(dataJson);

                result.Add(gismeteoMapper.Map(data));
            });

            await Task.WhenAll(tasks);

            return result;
        }
    }
}
