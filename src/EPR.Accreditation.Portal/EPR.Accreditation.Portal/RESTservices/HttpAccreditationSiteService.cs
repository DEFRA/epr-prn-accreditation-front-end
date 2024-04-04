using EPR.Accreditation.Portal.RESTservices.Interfaces;

namespace EPR.Accreditation.Portal.RESTservices
{
    public class HttpAccreditationSiteService : BaseHttpService, IHttpAccreditationSiteService
    {
        public HttpAccreditationSiteService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName) : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task<IEnumerable<string>> GetExemptionReferences(
            Guid id,
            Guid siteId)
        {
            return await Get<IEnumerable<string>>($"{id}/Site/{siteId}/ExemptionReferences");
        }

        public async Task UpdateExemptionReferences(
            Guid id,
            Guid siteId,
            IEnumerable<string> exemptionReferences)
        {
            await Put($"{id}/Site/{siteId}/ExemptionReferences", exemptionReferences);
        }
    }
}
