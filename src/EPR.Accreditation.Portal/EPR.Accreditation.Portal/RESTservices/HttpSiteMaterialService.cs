using EPR.Accreditation.Portal.Constants;
using EPR.Accreditation.Portal.RESTservices.Interfaces;

namespace EPR.Accreditation.Portal.RESTservices
{
    public class HttpSiteMaterialService : BaseHttpService, IHttpSiteMaterialService
    {
        public HttpSiteMaterialService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName) : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task<string> GetMeterialName(
            Guid id, 
            Guid siteId, 
            Guid materialId,
            Enums.Language language)
        {
            return await Get<string>($"{id}/Site/{siteId}/Material/{materialId}/Name?language={language}", false);
        }

        public async Task<string> GetWasteSource(
            Guid id, 
            Guid siteId, 
            Guid materialId)
        {
            return await Get<string>($"{id}/Site/{siteId}/Material/{materialId}");
        }

        public async Task UpdateWasteSource(
            Guid id, 
            Guid siteId, 
            Guid materialId, 
            string wasteSource)
        {
            await Put($"{id}/Site/{siteId}/Material/{materialId}", wasteSource);
        }
    }
}
