using EPR.Accreditation.Portal.Resources;
using EPR.Accreditation.Portal.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.CustomValidations.ExemptionReferences;
public class AllReferenceNumbersEmptyAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        //if (value is IList<ExemptionReferenceViewModel> list
        //    && list.All(item => string.IsNullOrWhiteSpace(item.Reference)))
        //    return new ValidationResult(ErrorMessage);

        //return ValidationResult.Success;

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