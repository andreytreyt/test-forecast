using System;
using System.ComponentModel.DataAnnotations;

namespace Forecast.Core.Models
{
    public class Weather : Entity
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [Required]
        public DateTime? Date { get; set; } = null;
        [Required]
        public double? Temperature { get; set; } = null;
        public WeatherType Type { get; set; }

        public override string ToString()
        {
            return $"{Date} {Temperature} {Type}";
        }
    }
}
