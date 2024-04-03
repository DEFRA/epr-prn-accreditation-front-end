using EPR.Accreditation.Portal.DTOs.AccreditationSite;
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

        public async Task<IEnumerable<ExemptionReference>> GetExemptionReferences(
            Guid id,
            Guid siteId)
        {
            return await Get<IEnumerable<ExemptionReference>>($"{id}/Site/{siteId}");
        }
    }
}
