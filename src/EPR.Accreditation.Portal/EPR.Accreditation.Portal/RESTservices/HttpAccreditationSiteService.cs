namespace EPR.Accreditation.Portal.RESTservices
{
    using EPR.Accreditation.Portal.Common.RESTservices;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;

    /// <summary>
    /// Service that connects to the Facade
    /// </summary>
    public class HttpAccreditationSiteService : BaseHttpService, IHttpAccreditationSiteService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpAccreditationSiteService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">Injecting the context accessor</param>
        /// <param name="httpClientFactory">Injecting the client factory</param>
        /// <param name="baseUrl">Declaring base URL</param>
        /// <param name="endPointName">Declaring the endpoint name</param>
        public HttpAccreditationSiteService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName)
            : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        /// <summary>
        /// Gets a list of Exemption references from the Facade
        /// </summary>
        /// <param name="id">Accreditation ID</param>
        /// <returns>The list of references</returns>
        public async Task<IEnumerable<string>> GetExemptionReferences(Guid id)
        {
            return await Get<IEnumerable<string>>($"{id}/Site/ExemptionReferences");
        }

        /// <summary>
        /// Updates the list of exemption references and passes to the Facade
        /// </summary>
        /// <param name="id">Accreditation ID</param>
        /// <param name="exemptionReferences">The list of updated references</param>
        /// <returns>Completed Task asynchronously</returns>
        public async Task UpdateExemptionReferences(
            Guid id,
            IEnumerable<string> exemptionReferences)
        {
            await Put($"{id}/Site/ExemptionReferences", exemptionReferences);
        }
    }
}
