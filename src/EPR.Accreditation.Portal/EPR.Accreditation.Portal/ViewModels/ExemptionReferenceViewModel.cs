using EPR.Accreditation.Portal.CustomValidations.ExemptionReferences;
using EPR.Accreditation.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class ExemptionReferenceViewModel
    {
        [AllReferenceNumbersEmpty(ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageBlank")]
        [RegularExpression(@"^[a-zA-Z0-9/]*$", ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageInvalidFormat")]
        [StringLength(20, ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageTooLong")]
        [UniqueReferenceNumber(ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageDuplicate")]
        public string Reference { get; set; }
    }
}
