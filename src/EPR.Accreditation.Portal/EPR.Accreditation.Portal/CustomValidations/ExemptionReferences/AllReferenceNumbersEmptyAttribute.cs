using EPR.Accreditation.Portal.Resources;
using EPR.Accreditation.Portal.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.CustomValidations.ExemptionReferences
{
    public class AllReferenceNumbersEmptyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var viewModel = (ExemptionReferencesViewModel)validationContext.ObjectInstance;

            // Check if all reference numbers are empty
            if (string.IsNullOrEmpty(viewModel.ReferenceNumber1) &&
                string.IsNullOrEmpty(viewModel.ReferenceNumber2) &&
                string.IsNullOrEmpty(viewModel.ReferenceNumber3) &&
                string.IsNullOrEmpty(viewModel.ReferenceNumber4) &&
                string.IsNullOrEmpty(viewModel.ReferenceNumber5))
            {
                return new ValidationResult(ErrorMessage ?? ExemptionReferencesResources.ErrorMessageBlank);
            }

            return ValidationResult.Success;
        }
    }
}
