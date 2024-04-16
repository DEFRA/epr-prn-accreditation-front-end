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
        /// <param name="httpContextAccessor"></param>
        /// <param name="httpClientFactory"></param>
        /// <param name="baseUrl"></param>
        /// <param name="endPointName"></param>
        public HttpAccreditationSiteService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName)
            : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task<IEnumerable<string>> GetExemptionReferences(Guid id)
        {
            return await Get<IEnumerable<string>>($"{id}/Site/ExemptionReferences");
        }

        public async Task UpdateExemptionReferences(
            Guid id,
            IEnumerable<string> exemptionReferences)
        {
            await Put($"{id}/Site/ExemptionReferences", exemptionReferences);
        }
    }
}
