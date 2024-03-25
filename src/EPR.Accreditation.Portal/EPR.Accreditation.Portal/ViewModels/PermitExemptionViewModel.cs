using EPR.Accreditation.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class PermitExemptionViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(PermitExemptionResources), ErrorMessageResourceName = "ErrorMessage")]
        public bool HasPermitExemption { get; set; }
    }
}