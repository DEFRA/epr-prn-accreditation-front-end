namespace EPR.Accreditation.Portal.RESTservices
{
    using EPR.Accreditation.Facade.Common.RESTservices;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.DTOs.MaterialReprocessorDetails;
    using EPR.Accreditation.Portal.DTOs.WastePermit;
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;

    public class HttpSiteMaterialService : BaseHttpService, IHttpSiteMaterialService
    {
        public HttpSiteMaterialService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName)
            : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task<string> GetMeterialName(
            Guid id,
            Guid? siteId,
            Guid materialId,
            Enums.Language language)
        {
            var sitePart = siteId.HasValue ? $"OverseasSite/{siteId}" : "Site";
            return await Get<string>($"{id}/{sitePart}/Material/{materialId}/Name?language={language}", false);
        }

        public async Task<string> GetWasteSource(
            SiteType siteType,
            Guid id,
            Guid? siteId,
            Guid materialId)
        {
            var site = GetSiteName(
                siteType,
                siteId);
            return await Get<string>($"{id}/{site}/Material/{materialId}/WasteSource");
        }

        public async Task UpdateWasteSource(
            SiteType siteType,
            Guid id,
            Guid? siteId,
            Guid materialId,
            string wasteSource)
        {
            var site = GetSiteName(siteType, siteId);
            await Put($"{id}/{site}/Material/{materialId}/WasteSource", wasteSource);
        }

        public async Task<NonWasteInputsDto> GetNonWasteInputs(
            Guid id,
            Guid materialId)
        {
            return await Get<NonWasteInputsDto>($"{id}/Site/Material/{materialId}/NonWasteInputs");
        }

        public async Task UpdateNonWasteInputs(
            Guid id,
            Guid materialId,
            NonWasteInputsDto nonWasteInputsDto)
        {
            await Put<NonWasteInputsDto>($"{id}/Site/Material/{materialId}/NonWasteInputs", nonWasteInputsDto);
        }

        public async Task<MaterialOutputsDto> GetMaterialOutputs(
            Guid id,
            Guid materialId)
        {
            return await Get<MaterialOutputsDto>($"{id}/Site/Material/{materialId}/MaterialOutputs");
        }

        public async Task UpdateMaterialOutputs(
            Guid id,
            Guid materialId,
            MaterialOutputsDto materialOutputsDto)
        {
            await Put($"{id}/Site/Material/{materialId}/MaterialOutputs", materialOutputsDto);
        }

        /// <summary>
        /// Gets material waste output.
        /// </summary>
        /// <param name="id">Accreditation id.</param>
        /// <param name="materialId">Material id.</param>
        /// <returns>Material waste output dto.</returns>
        public async Task<MaterialWasteOutputsDto> GetMaterialWasteOutputs(
            Guid id,
            Guid materialId)
        {
            return await this.Get<MaterialWasteOutputsDto>($"{id}/Site/Material/{materialId}/MaterialWasteOutputs");
        }

        /// <summary>
        /// Updates material waste output.
        /// </summary>
        /// <param name="id">Accreditation id.</param>
        /// <param name="materialId">Material id.</param>
        /// <param name="materialWasteOutputsDto">Material waste output dto.</param>
        /// <returns>Nothing.</returns>
        public async Task UpdateMaterialWasteOutputs(
            Guid id,
            Guid materialId,
            MaterialWasteOutputsDto materialWasteOutputsDto)
        {
            await this.Put($"{id}/Site/Material/{materialId}/MaterialWasteOutputs", materialWasteOutputsDto);
        }

        // =====================================================


        public async Task<bool?> GetReprocessedWasteLastYear(
            Guid id,
            Guid materialId)
        {
            return await Get<bool?>($"{id}/Site/Material/{materialId}/WasteLastYear");
        }

        public async Task UpdateReprocessedWasteLastYear(
            Guid id,
            Guid materialId,
            ReprocessedWasteLastYear reprocessedWasteLastYear)
        {
            await Put($"{id}/Site/Material/{materialId}/WasteLastYear", reprocessedWasteLastYear);
        }

        public async Task<bool?> GetHasPermitExemption(Guid id)
        {
            return await Get<bool?>($"{id}/WastePermitExemption");
        }

        public async Task UpdatePermitExemption(Guid id, PermitExemption permitExemption)
        {
            await Put($"{id}/WastePermitExemption", permitExemption);
        }

        private string GetSiteName(
            SiteType siteType,
            Guid? siteId) => siteType == SiteType.Site ? "Site" : $"OverseasSite/{siteId}";
    }
}
