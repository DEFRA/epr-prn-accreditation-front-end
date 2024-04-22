using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    public interface ISiteService
    {
        Task<SiteAddressViewModel> GetSiteAddressViewModel(Guid id, Guid siteId, Guid materialId);
        Task SaveSiteAddress(SiteAddressViewModel siteAddressViewModel);
    }
}
