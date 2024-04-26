namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    using EPR.Accreditation.Portal.ViewModels;

    /// <summary>
    /// ISiteService Interface.
    /// </summary>
    public interface ISiteService
    {
        /// <summary>
        /// Returns the model for Site Address.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>ViewModel</returns>
        Task<SiteAddressViewModel> GetSiteAddressViewModel(Guid id);

        /// <summary>
        /// Saves the site address.
        /// </summary>
        /// <param name="siteAddressViewModel">ViewModel</param>
        /// <returns>Void</returns>
        Task SaveSiteAddress(SiteAddressViewModel siteAddressViewModel);
    }
}
