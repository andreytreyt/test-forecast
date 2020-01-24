using System.ComponentModel.DataAnnotations;

namespace Forecast.Core.Models
{
    public class WeatherType : Entity
    {
        [Required]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
