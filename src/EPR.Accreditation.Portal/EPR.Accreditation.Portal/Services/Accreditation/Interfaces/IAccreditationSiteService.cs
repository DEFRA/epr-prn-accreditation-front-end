using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    public interface IAccreditationSiteService
    {
        Task<ExemptionReferencesViewModel> GetExemptionReferencesViewModel(
            Guid id,
            Guid siteId);

        Task UpdateExemptionReferences(ExemptionReferencesViewModel exemptionReferencesViewModel);
    }
}
