namespace EPR.Accreditation.Portal.Attributes.Validation;

using System.ComponentModel.DataAnnotations;
using EPR.Accreditation.Portal.Resources;
using EPR.Accreditation.Portal.ViewModels;

public class AllReferenceNumbersEmptyAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var viewModel = (ExemptionReferencesViewModel)validationContext.ObjectInstance;

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