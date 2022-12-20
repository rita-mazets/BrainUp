using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BrainUp.Attributes
{

    public class CurrentDateAttribute : ValidationAttribute
    {

        public string GetErrorMessage() =>
            $"The date cannot be less than the current one.";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            if ((DateTime)value < DateTime.Now)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
