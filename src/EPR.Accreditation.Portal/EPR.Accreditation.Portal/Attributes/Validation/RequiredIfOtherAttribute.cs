namespace EPR.Accreditation.Portal.Attributes.Validation
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Validate attribute to ensure that given an entry in one field, another field
    /// should also have an entry to be considered valid.
    /// </summary>
    public class RequiredIfOtherAttribute : ValidationAttribute
    {
        private readonly string _propertyName;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfOtherAttribute"/> class.
        /// Constructor
        /// </summary>
        /// <param name="propertyName">the name of the other property on the model to check.</param>
        public RequiredIfOtherAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        /// <summary>
        /// Override for formatting the error message.
        /// </summary>
        /// <param name="name">Name of the field to display an error for.</param>
        /// <returns>Formatted error message.</returns>
        public override string FormatErrorMessage(string name)
        {
            // Example of using a resource file for error messages
            return string.Format(ErrorMessageString, name);
        }

        /// <summary>
        /// Determines if the attribute it is attached to is valid or not.
        /// </summary>
        /// <param name="value">The value of the object this attribute is attached to.</param>
        /// <param name="validationContext">the validation context for this attribute.</param>
        /// <returns>The validation result indicating, with error, the state of the model property.</returns>
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var otherPropertyValue = validationContext.ObjectType.GetProperty(_propertyName)?.GetValue(validationContext.ObjectInstance, null);

            if (value != null)
            {
                if (otherPropertyValue == null || string.IsNullOrWhiteSpace(otherPropertyValue.ToString()))
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);

                    return new ValidationResult(errorMessage ?? $"{validationContext.DisplayName} requires {_propertyName} to have a value.");
                }
            }

            return ValidationResult.Success;
        }
    }
}