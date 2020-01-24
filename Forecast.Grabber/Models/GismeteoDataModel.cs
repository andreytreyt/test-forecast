using System.Collections.Generic;
using Newtonsoft.Json;

namespace Forecast.Grabber.Models
{
    internal class GismeteoDataModel
    {
        [JsonProperty("city_name")]
        public string City { get; set; }
        [JsonProperty("data")]
        public List<List<object>> Data { get; set; }
    }
}
