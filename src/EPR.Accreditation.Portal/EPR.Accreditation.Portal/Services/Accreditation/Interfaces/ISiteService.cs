namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    using EPR.Accreditation.Portal.ViewModels;

    public interface ISiteService
    {
        Task<SiteAddressViewModel> GetSiteAddressViewModel(Guid id);

        Task SaveSiteAddress(SiteAddressViewModel siteAddressViewModel);
    }
}
