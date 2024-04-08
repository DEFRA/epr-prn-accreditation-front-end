using EPR.Accreditation.Portal.CustomValidations.ExemptionReferences;
using EPR.Accreditation.Portal.Resources;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class ExemptionReferencesViewModel
    {
        public Guid Id { get; set; }

        [AllReferenceNumbersEmpty(ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageBlank")]
        [UniqueReferenceNumber(ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageDuplicate")]
        public IList<ExemptionReferenceViewModel> ExemptionReferencesVm { get; set; }
    }
}
