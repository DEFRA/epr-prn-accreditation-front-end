using EPR.Accreditation.Portal.DTOs.AccreditationSite;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class ExemptionReferencesViewModel
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public IEnumerable<ExemptionReference> ExemptionReferences { get; set; }
    }
}
