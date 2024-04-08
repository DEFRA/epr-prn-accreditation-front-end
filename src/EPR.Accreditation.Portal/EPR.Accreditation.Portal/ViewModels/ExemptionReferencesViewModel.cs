using EPR.Accreditation.Portal.CustomValidations.ExemptionReferences;
using EPR.Accreditation.Portal.Resources;

namespace EPR.Accreditation.Portal.ViewModels
{
    [AllReferenceNumbersEmpty(ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageBlank")]
    public class ExemptionReferencesViewModel
    {
        public Guid Id { get; set; }

        [AllReferenceNumbersEmpty(ErrorMessage = "All reference numbers are empty.")]
        [UniqueReferenceNumber(ErrorMessage = "Reference numbers must be unique.")]
        public IList<ExemptionReferenceViewModel> ExemptionReferencesVm { get; set; }
    }
}
