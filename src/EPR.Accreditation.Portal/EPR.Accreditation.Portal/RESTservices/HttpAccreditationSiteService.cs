namespace EPR.Accreditation.Portal.RESTservices
{
    using EPR.Accreditation.Portal.Common.RESTservices;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;

    public class HttpAccreditationSiteService : BaseHttpService, IHttpAccreditationSiteService
    {
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
