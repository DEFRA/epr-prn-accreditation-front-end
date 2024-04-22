namespace EPR.Accreditation.Portal.RESTservices
{
    using EPR.Accreditation.Portal.Common.Dtos;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.Common.RESTservices;
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

        public async Task<ReprocessingSupportingInformationDto> GetNonWasteInputs(
            Guid id,
            Guid materialId)
        {
            return await Get<ReprocessingSupportingInformationDto>($"{id}/Site/Material/{materialId}/NonWasteInputs");
        }

        public async Task UpdateNonWasteInputs(
            Guid id,
            Guid materialId,
            ReprocessingSupportingInformationDto nonWasteInputsDto)
        {
            await Put<ReprocessingSupportingInformationDto>($"{id}/Site/Material/{materialId}/NonWasteInputs", nonWasteInputsDto);
        }

        public async Task<ReprocessingSupportingInformationDto> GetProductsProduced(
            Guid id,
            Guid materialId)
        {
            return await Get<ReprocessingSupportingInformationDto>($"{id}/Site/Material/{materialId}/ProductsProduced");
        }

        public async Task UpdateProductsProduced(
            Guid id,
            Guid materialId,
            ReprocessingSupportingInformationDto nonWasteInputsDto)
        {
            await Put<ReprocessingSupportingInformationDto>($"{id}/Site/Material/{materialId}/ProductsProduced", nonWasteInputsDto);
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

        public async Task<AccreditationMaterial> GetAccreditationMaterial(
            Guid id,
            Guid siteId,
            Guid materialExternalId)
        {
            return await Get<AccreditationMaterial>($"{id}/Site/{siteId}/Material/{materialExternalId}");
        }

        public async Task UpdateAccreditationMaterial(
            Guid accreditationExternalId,
            Guid siteId,
            Guid materialExternalId,
            AccreditationMaterial accreditationMaterial)
        {
            await Put($"{accreditationExternalId}/Site/{siteId}/Material/{materialExternalId}", accreditationMaterial);
        }

        /// <summary>
        /// Gets the waste description codes for the accreditation material
        /// from the facade API
        /// </summary>
        /// <param name="id">The accreditation id</param>
        /// <param name="siteId">The id of the overseas site</param>
        /// <param name="materialId">The material id</param>
        /// <returns>The waste description code dto</returns>
        public async Task<List<string>> GetWasteDescriptionCodes(
            Guid id,
            Guid siteId,
            Guid materialId)
        {
            return await Get<List<string>>($"{id}/OverseasSite/{siteId}/Material/{materialId}/WasteDescriptionCodes");
        }

        /// <summary>
        /// Posts the waste description codes to the facade API for adding or removing
        /// to the material for an accreditation
        /// </summary>
        /// <param name="id">Id of the accreditation</param>
        /// <param name="siteId">The id of the overseas site</param>
        /// <param name="materialId">Id of the material that the waste description codes are for</param>
        /// <param name="wasteDecriptionCodes">The list of waste description codes</param>
        /// <returns>Async task</returns>
        public async Task SaveWasteDescriptionCodes(
            Guid id,
            Guid siteId,
            Guid materialId,
            IEnumerable<string> wasteDecriptionCodes)
        {
            await Post($"{id}/OverseasSite/{siteId}/Material/{materialId}/WasteDescriptionCodes", wasteDecriptionCodes);
        }

        private string GetSiteName(
            SiteType siteType,
            Guid? siteId) => siteType == SiteType.Site ? "Site" : $"OverseasSite/{siteId}";
    }
}