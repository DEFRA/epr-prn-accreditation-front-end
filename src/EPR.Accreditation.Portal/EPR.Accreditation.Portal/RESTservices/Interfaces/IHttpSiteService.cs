namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    using EPR.Accreditation.Portal.DTOs.Site;

    /// <summary>
    /// IHttpSiteService.
    /// </summary>
    public interface IHttpSiteService
    {
        /// <summary>
        /// GetSite.
        /// </summary>
        /// <param name="accredititionId">accredititionId</param>
        /// <param name="siteExternalId">siteExternalId</param>
        /// <returns>Site</returns>
        Task<Site> GetSite(Guid accredititionId, Guid siteExternalId);

        /// <summary>
        /// CreateSite.
        /// </summary>
        /// <param name="accreditationExternalId">accreditationExternalId</param>
        /// <param name="site">site</param>
        /// <returns>Guid</returns>
        Task<Guid> CreateSite(Guid accreditationExternalId, Site site);

        /// <summary>
        /// UpdateSite
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="site">site</param>
        /// <returns>Task</returns>
        Task UpdateSite(Guid id, Site site);
    }
}