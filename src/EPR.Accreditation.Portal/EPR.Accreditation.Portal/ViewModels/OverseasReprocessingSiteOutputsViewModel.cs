namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;

    public class OverseasReprocessingSiteOutputsViewModel
    {
        public Guid Id { get; set; }

        public Guid ExternalId { get; set; }

        [Required(ErrorMessageResourceType = typeof(OverseasSiteOutputsResources), ErrorMessageResourceName = "NoOutputsSupplied")]
        [StringLength(500)]
        public string Outputs { get; set; }
    }
}
