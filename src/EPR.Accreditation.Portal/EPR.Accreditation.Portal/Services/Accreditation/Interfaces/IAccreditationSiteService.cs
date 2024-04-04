using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    public interface IAccreditationSiteService
    {
        Task<ExemptionReferencesViewModel> GetExemptionReferencesViewModel(Guid id);

        Task UpdateExemptionReferences(ExemptionReferencesViewModel exemptionReferencesViewModel);
    }
}
