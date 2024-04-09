using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.Validation
{
    public class RequireBothFieldsAttribute : ValidationAttribute
    {
        private string _otherProperty;

        public RequireBothFieldsAttribute(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(
            object value, 
            ValidationContext validationContext)
        {
            var otherPropertyValue = validationContext.ObjectType.GetProperty(_otherProperty)?.GetValue(validationContext.ObjectInstance, null);

            if (value != null)
            {
                if (otherPropertyValue == null || string.IsNullOrWhiteSpace(otherPropertyValue.ToString()))
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} requires {_otherProperty} to have a value.");
                }
            }

            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            // Example of using a resource file for error messages
            return string.Format(ErrorMessageString, name);
        }
    }
}