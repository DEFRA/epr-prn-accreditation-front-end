namespace EPR.Accreditation.Portal.Attributes.Validation
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.ViewModels;

    /// <summary>
    /// Custom validation attribute to check if at least one field has been completed
    /// </summary>
    public class AllReferenceNumbersEmptyAttribute : ValidationAttribute
    {
        /// <summary>
        /// Checking if the model state is valid according to the below condition
        /// </summary>
        /// <param name="value">Value data being passed</param>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Success or Fail</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var viewModel = (ExemptionReferencesViewModel)validationContext.ObjectInstance;

            // Check if all reference numbers are empty
            if (string.IsNullOrEmpty(viewModel.Reference1) &&
                string.IsNullOrEmpty(viewModel.Reference2) &&
                string.IsNullOrEmpty(viewModel.Reference3) &&
                string.IsNullOrEmpty(viewModel.Reference4) &&
                string.IsNullOrEmpty(viewModel.Reference5))
            {
                return new ValidationResult(ErrorMessage ?? ExemptionReferencesResources.ErrorMessageBlank);
            }

            return ValidationResult.Success;
        }
    }
}