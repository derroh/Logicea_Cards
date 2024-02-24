using System.ComponentModel.DataAnnotations;

namespace Logicea_Cards.Validation
{
    public class AlphanumericColorCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string colorCode = value as string;
            if (colorCode == null)
            {
                // Returning success if the value is null, you might want to change this behavior based on your requirements
                return ValidationResult.Success;
            }

            // Regular expression to match alphanumeric color codes
            // Example: #RRGGBB or #RGB
            if (!System.Text.RegularExpressions.Regex.IsMatch(colorCode, @"^#[0-9a-fA-F]{3}(?:[0-9a-fA-F]{3})?$"))
            {
                return new ValidationResult("Invalid color code format. Color code must be in the format #RRGGBB or #RGB.");
            }

            return ValidationResult.Success;
        }
    }
}
