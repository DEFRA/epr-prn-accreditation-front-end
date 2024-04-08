using EPR.Accreditation.Portal.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.CustomValidations.ExemptionReferences
{
    public class UniqueReferenceNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as IList<ExemptionReferenceViewModel>;

            if (list != null && list.GroupBy(x => x.Reference).Any(g => g.Count() > 1))
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
