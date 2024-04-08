using EPR.Accreditation.Portal.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.CustomValidations.ExemptionReferences;
public class AllReferenceNumbersEmptyAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is IList<ExemptionReferenceViewModel> list
            && list.All(item => string.IsNullOrWhiteSpace(item.Reference)))
            return new ValidationResult(ErrorMessage);

        return ValidationResult.Success;
    }
}