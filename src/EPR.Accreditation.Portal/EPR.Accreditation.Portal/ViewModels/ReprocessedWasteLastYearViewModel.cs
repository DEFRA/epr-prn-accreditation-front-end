using EPR.Accreditation.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class ReprocessedWasteLastYearViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(PermitExemptionResources), ErrorMessageResourceName = "ErrorMessage")]
        public bool? HasReprocessedWasteLastYear { get; set; }
    }
}
