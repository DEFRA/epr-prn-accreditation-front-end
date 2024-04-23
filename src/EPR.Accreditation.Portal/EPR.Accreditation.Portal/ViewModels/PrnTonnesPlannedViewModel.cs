namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Common.Enums;
    using EPR.Accreditation.Portal.Resources;

    public class PrnTonnesPlannedViewModel
    {
        public Guid ExternalId { get; set; }

        [Required(ErrorMessageResourceName = "MissingSelectionErrorMessage", ErrorMessageResourceType = typeof(PrnTonnesPlannedResources))]
        public PrnPlannedTonnesType? PrnPlannedTonnesType { get; set; }

        public decimal? PrnPlannedTonnesFee { get; set; }
    }
}
