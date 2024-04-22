namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    using EPR.Accreditation.Portal.ViewModels;

    public interface ISiteService
    {
        Task<SiteAddressViewModel> GetSiteAddressViewModel(Guid id, Guid siteId, Guid materialId);

        Task SaveSiteAddress(SiteAddressViewModel siteAddressViewModel);
    }
}
