using EPR.Accreditation.Portal.Resources;
using EPR.Accreditation.Portal.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.CustomValidations.ExemptionReferences
{
    public class UniqueReferenceNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var viewModel = (ExemptionReferencesViewModel)validationContext.ObjectInstance;
            var referenceNumber = (string)value;

            if (string.IsNullOrEmpty(referenceNumber))
                // Reference number is not required, so no need to check for duplicates if it's empty
                return ValidationResult.Success;

            // Removing the current reference number from the list before checking for duplicates
            if (viewModel.ExemptionReferencesVm.Any(rn => rn.Reference == referenceNumber))
                return new ValidationResult(ErrorMessage ?? ExemptionReferencesResources.ErrorMessageDuplicate);

            return ValidationResult.Success;
        }
    }
}
