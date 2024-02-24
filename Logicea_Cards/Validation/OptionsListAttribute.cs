using System.ComponentModel.DataAnnotations;

namespace Logicea_Cards.Validation
{
    public class OptionsListAttribute : ValidationAttribute
    {
        private readonly string[] _options;

        public OptionsListAttribute(params string[] options)
        {
            _options = options;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !_options.Contains(value.ToString()))
            {
                return new ValidationResult($"The field {validationContext.DisplayName} must be one of the following options: {string.Join(", ", _options)}.");
            }

            return ValidationResult.Success;
        }
    }
}
