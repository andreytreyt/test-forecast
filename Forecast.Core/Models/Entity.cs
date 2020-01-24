using System.ComponentModel.DataAnnotations;

namespace Forecast.Core.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public bool IsValid()
        {
            var validationContext = new ValidationContext(this);
            return Validator.TryValidateObject(this, validationContext, null, true);
        }
    }
}
