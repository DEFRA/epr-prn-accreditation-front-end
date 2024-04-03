using EPR.Accreditation.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class ExemptionReferencesViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageBlank")]
        [RegularExpression(@"^[a-zA-Z0-9/]*$", ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageInvalidFormat")]
        [StringLength(20, ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageTooLong")]
        public string ReferenceNumber1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageBlank")]
        [RegularExpression(@"^[a-zA-Z0-9/]*$", ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageInvalidFormat")]
        [StringLength(20, ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageTooLong")]
        public string ReferenceNumber2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageBlank")]
        [RegularExpression(@"^[a-zA-Z0-9/]*$", ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageInvalidFormat")]
        [StringLength(20, ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageTooLong")]
        public string ReferenceNumber3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageBlank")]
        [RegularExpression(@"^[a-zA-Z0-9/]*$", ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageInvalidFormat")]
        [StringLength(20, ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageTooLong")]
        public string ReferenceNumber4 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageBlank")]
        [RegularExpression(@"^[a-zA-Z0-9/]*$", ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageInvalidFormat")]
        [StringLength(20, ErrorMessageResourceType = typeof(ExemptionReferencesResources), ErrorMessageResourceName = "ErrorMessageTooLong")]
        public string ReferenceNumber5 { get; set; }
    }
}
