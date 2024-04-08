using EPR.Accreditation.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class ExemptionReferenceViewModel
    {
        [RegularExpression(@"^[a-zA-Z0-9/]*$", ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageInvalidFormat")]
        [StringLength(20, ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageTooLong")]
        public string Reference { get; set; }
    }
}
