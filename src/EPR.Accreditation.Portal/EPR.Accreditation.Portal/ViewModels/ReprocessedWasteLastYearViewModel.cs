namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;

    public class ReprocessedWasteLastYearViewModel
    {
        public Guid Id { get; set; }

        public Guid MaterialId { get; set; }

        [Required(ErrorMessageResourceType = typeof(PermitExemptionResources), ErrorMessageResourceName = "ErrorMessage")]
        public bool? HasReprocessedWasteLastYear { get; set; }
    }
}
