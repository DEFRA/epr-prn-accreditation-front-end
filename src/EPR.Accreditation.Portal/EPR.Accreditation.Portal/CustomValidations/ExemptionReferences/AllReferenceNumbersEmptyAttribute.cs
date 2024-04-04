using EPR.Accreditation.Portal.Resources;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.CustomValidations.ExemptionReferences
{
    public class AllReferenceNumbersEmptyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var viewModel = (ExemptionReferencesViewModel)validationContext.ObjectInstance;

            return viewModel.ExemptionReferencesVm.All(x => x.Reference.IsNullOrEmpty()) ?
                new ValidationResult(ErrorMessage ?? ExemptionReferencesResources.ErrorMessageBlank)
                : ValidationResult.Success;
        }
    }
}
