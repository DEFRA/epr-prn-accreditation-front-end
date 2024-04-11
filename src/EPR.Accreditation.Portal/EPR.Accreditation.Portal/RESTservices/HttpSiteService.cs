using EPR.Accreditation.Portal.RESTservices.Interfaces;


namespace EPR.Accreditation.Portal.RESTservices
{
    public class HttpSiteService : BaseHttpService, IHttpSiteService
    {
        public HttpSiteService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName) : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task<DTOs.Site.Site> GetSite(Guid id, Guid siteId)
        {
            return await Get<DTOs.Site.Site>($"{id}/Site?siteExternalId={siteId}", false);
        }

        public async Task<Guid> CreateSite(Guid id, DTOs.Site.Site site)
        {
            return await Post<Guid>($"{id}/Site", site);
        }
    }
}
