namespace EPR.Accreditation.Portal.Attributes.Validation
{
    using System.Collections;
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.ViewModels.Interfaces;

    /// <summary>
    /// Class to perform validation against an list (IEnumerable)
    /// to ensure it is populated with at least one item
    /// </summary>
    public class ListMustBePopulatedAttribute : ValidationAttribute
    {
        /// <summary>
        /// Override of the base FormatErrorMessage
        /// </summary>
        /// <param name="name">parameter not used in this case</param>
        /// <returns>The error message from the specified resource file</returns>
        public override string FormatErrorMessage(string name)
        {
            if (!string.IsNullOrWhiteSpace(ErrorMessageResourceName) &&
                ErrorMessageResourceType != null)
            {
                var resourceManager = new System.Resources.ResourceManager(ErrorMessageResourceType);
                return resourceManager.GetString(ErrorMessageResourceName);
            }

            // Example of using a resource file for error messages
            return string.Format(ErrorMessageString, name);
        }

        /// <summary>
        /// Performs the validation against the list
        /// </summary>
        /// <param name="value">Value that the attribute is present against</param>
        /// <param name="validationContext">The validation context</param>
        /// <returns>A validation result for the property this attribute is applied to</returns>
        /// <exception cref="NullReferenceException">When the property is not a list (IEnumerable)</exception>
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            // cast value to a list, as this is for lists only
            var valueList = value as IEnumerable;

            if (valueList == null)
            {
                throw new NullReferenceException("Object must be a list");
            }

            foreach (var item in valueList)
            {
                if (item is IEntryMade entryMade &&
                    entryMade.EntryMade)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
