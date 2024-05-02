namespace EPR.Accreditation.Portal.Attributes.Validation
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class to validate the numnber of characters is not exceeded in a
    /// numeric fields
    /// </summary>
    public class NumericCharacterLengthAttribute : ValidationAttribute
    {
        public int MaxLength { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericCharacterLengthAttribute"/> class.
        /// </summary>
        /// <param name="maxLength">value indicating the maximum number of characters</param>
        /// <exception cref="InvalidOperationException">Thrown if the maxlength value is 0 or less</exception>
        public NumericCharacterLengthAttribute(int maxLength)
        {
            if (maxLength <= 0)
            {
                throw new InvalidOperationException("Max length must be a non zero positive value");
            }

            MaxLength = maxLength;
        }

        /// <summary>
        /// Override for formatting the error message.
        /// </summary>
        /// <param name="name">Name of the field to display an error for.</param>
        /// <returns>Formatted error message.</returns>
        public override string FormatErrorMessage(string name)
        {
            // Example of using a resource file for error messages
            return string.Format(string.Format(ErrorMessageString, MaxLength), name);
        }

        /// <summary>
        /// This doesn't actually do anything. The adapter that goes with it ensures
        /// a maxlength attribute is added to the field that uses the property this
        /// validation attribute is applied to
        /// </summary>
        /// <param name="value">The value to validate against</param>
        /// <param name="validationContext">The validation context</param>
        /// <returns>A ValidationResult determining the result of whether it is valid or not</returns>
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            return ValidationResult.Success;
        }
    }
}
