using EPR.Accreditation.Portal.Resources;
using EPR.Accreditation.Portal.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.CustomValidations.ExemptionReferences
{
    public class UniqueReferenceNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //if (value is IList<ExemptionReferenceViewModel> list)
            //{
            //    var nonEmptyReferences = list.Where(x => !string.IsNullOrWhiteSpace(x.Reference));

            //    if (nonEmptyReferences.GroupBy(x => x.Reference).Any(g => g.Count() > 1))
            //    {
            //        return new ValidationResult(ErrorMessage);
            //    }
            //}
            //return ValidationResult.Success;

            var viewModel = (ExemptionReferencesViewModel)validationContext.ObjectInstance;
            var referenceNumber = (string)value;

            if (string.IsNullOrEmpty(referenceNumber))
            {
                // Reference number is not required, so no need to check for duplicates if it's empty
                return ValidationResult.Success;
            }

            // Checking if the entered reference number already exists in any of the other reference number properties
            var allReferenceNumbers = new List<string>
        {
            viewModel.Reference1,
            viewModel.Reference2,
            viewModel.Reference3,
            viewModel.Reference4,
            viewModel.Reference5
        };

            // Removing the current reference number from the list before checking for duplicates
            allReferenceNumbers.Remove(referenceNumber);

            if (allReferenceNumbers.Any(rn => rn == referenceNumber))
            {
                return new ValidationResult(ErrorMessage ?? ExemptionReferencesResources.ErrorMessageDuplicate);
            }

            return ValidationResult.Success;
        }
    }
}
