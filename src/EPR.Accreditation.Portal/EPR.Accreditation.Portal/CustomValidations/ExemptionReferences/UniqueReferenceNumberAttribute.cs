using EPR.Accreditation.Portal.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.CustomValidations.ExemptionReferences
{
    public class UniqueReferenceNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IList<ExemptionReferenceViewModel> list)
            {
                var nonEmptyReferences = list.Where(x => !string.IsNullOrWhiteSpace(x.Reference));

                if (nonEmptyReferences.GroupBy(x => x.Reference).Any(g => g.Count() > 1))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
