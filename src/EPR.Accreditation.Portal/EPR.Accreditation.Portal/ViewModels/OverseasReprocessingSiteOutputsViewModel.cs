using EPR.Accreditation.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class OverseasReprocessingSiteOutputsViewModel
    {
        public Guid Id { get; set; }

        public Guid ExternalId { get; set; }

        [Required(ErrorMessageResourceType = typeof(OverseasSiteOutputsResources), ErrorMessageResourceName = "NoOutputsSupplied")]
        [StringLength(500)]
        public string Outputs { get; set; }
    }
}
