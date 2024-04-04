using EPR.Accreditation.Portal.Common.Dtos.Portal;
using EPR.Accreditation.Portal.Enums;
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
            SiteType siteType,
            Guid id, 
            Guid siteId, 
            Guid materialId)
        {
            var site = GetSiteName(siteType);
            return await Get<string>($"{id}/{site}/{siteId}/Material/{materialId}/WasteSource");
        }

        public async Task UpdateWasteSource(
            SiteType siteType,
            Guid id, 
            Guid siteId, 
            Guid materialId, 
            string wasteSource)
        {
            var site = GetSiteName(siteType);
            await Put($"{id}/{site}/{siteId}/Material/{materialId}/WasteSource", wasteSource);
        }

        public async Task<MaterialOutputsDto> GetMaterialOutputs(
            Guid id, 
            Guid siteId, 
            Guid materialId)
        {
            return await Get<MaterialOutputsDto>($"{id}/Site/{siteId}/Material/{materialId}/MaterialOutputs");
        }

        public async Task UpdateMaterialOutputs(
            Guid id, 
            Guid siteId, 
            Guid materialId, 
            MaterialOutputsDto materialOutputsDto)
        {
            await Put($"{id}/Site/{siteId}/Material/{materialId}/MaterialOutputs", materialOutputsDto);
        }

        private string GetSiteName(SiteType siteType) => siteType == SiteType.Site ? "Site" : "OverseasSite";
    }
}
