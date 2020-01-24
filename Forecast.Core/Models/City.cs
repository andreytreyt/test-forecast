using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forecast.Core.Models
{
    public class City : Entity
    {
        [Required]
        public string Name { get; set; }
        public virtual List<Weather> Weathers { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
