namespace EPR.Accreditation.Portal.Attributes.Validation
{
    using EPR.Accreditation.Portal.ViewModels.Interfaces;
    using System.Collections;
    using System.ComponentModel.DataAnnotations;

    public class ListMustBePopulatedAttribute : ValidationAttribute
    {
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

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            // cast value to a list, as this is for lists only
            var valueList = value as IEnumerable;

            if (valueList == null)
                throw new ArgumentNullException(nameof(value), "Object must be a list");

            foreach (var item in valueList )
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
