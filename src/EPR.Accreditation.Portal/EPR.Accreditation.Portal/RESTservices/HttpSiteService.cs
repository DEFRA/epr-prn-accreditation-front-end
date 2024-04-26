namespace EPR.Accreditation.Portal.RESTservices
{
    using EPR.Accreditation.Portal.Common.RESTservices;
    using EPR.Accreditation.Portal.DTOs.Site;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;

    /// <summary>
    /// HttpSiteService for accesing site services.
    /// </summary>
    public class HttpSiteService : BaseHttpService, IHttpSiteService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpSiteService"/> class.
        /// HttpSiteService.
        /// </summary>
        /// <param name="httpContextAccessor">httpContextAccessor</param>
        /// <param name="httpClientFactory">httpClientFactory</param>
        /// <param name="baseUrl">baseUrl</param>
        /// <param name="endPointName">endPointName</param>
        public HttpSiteService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName)
            : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        /// <summary>
        /// Get a site dto from the backend.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Task<DTOs.Site.Site></returns>
        public async Task<Site> GetSite(Guid id)
        {
            return await Get<DTOs.Site.Site>($"{id}/Site", false);
        }

        /// <summary>
        /// CreateSite
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="site">site.</param>
        /// <returns>Task<Guid></returns>
        public async Task<Guid> CreateSite(Guid id, DTOs.Site.Site site)
        {
            return await Post<Guid>($"{id}/Site", site);
        }

        /// <summary>
        /// UpdateSite.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="site">site</param>
        /// <returns>Task</returns>
        public async Task UpdateSite(Guid id, DTOs.Site.Site site)
        {
            await Put($"{id}/Site", site);
        }
    }
}
