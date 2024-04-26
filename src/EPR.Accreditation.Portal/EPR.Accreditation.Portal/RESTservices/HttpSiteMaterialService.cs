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
            SiteType siteType,
            Guid id,
            Guid materialId,
            Enums.Language language)
        {
            var site = GetSiteName(siteType);
            return await Get<string>($"{id}/{site}/{materialId}/Name?language={language}", false);
        }

        public async Task<string> GetWasteSource(
            SiteType siteType,
            Guid id,
            Guid? siteId,
            Guid materialId)
        {
            var site = GetSiteName(siteType);
            return await Get<string>($"{id}/{site}/{materialId}/WasteSource");
        }

        public async Task UpdateWasteSource(
            SiteType siteType,
            Guid id,
            Guid? siteId,
            Guid materialId,
            string wasteSource)
        {
            var site = GetSiteName(siteType);
            await Put($"{id}/{site}/{materialId}/WasteSource", wasteSource);
        }

        public async Task<ReprocessingSupportingInformationDto> GetNonWasteInputs(
            Guid id,
            Guid materialId)
        {
            return await Get<ReprocessingSupportingInformationDto>($"{id}/Material/{materialId}/NonWasteInputs");
        }

        public async Task UpdateNonWasteInputs(
            Guid id,
            Guid materialId,
            ReprocessingSupportingInformationDto nonWasteInputsDto)
        {
            await Put<ReprocessingSupportingInformationDto>($"{id}/Material/{materialId}/NonWasteInputs", nonWasteInputsDto);
        }

        public async Task<ReprocessingSupportingInformationDto> GetProductsProduced(
            Guid id,
            Guid materialId)
        {
            return await Get<ReprocessingSupportingInformationDto>($"{id}/Material/{materialId}/ProductsProduced");
        }

        public async Task UpdateProductsProduced(
            Guid id,
            Guid materialId,
            ReprocessingSupportingInformationDto nonWasteInputsDto)
        {
            await Put<ReprocessingSupportingInformationDto>($"{id}/Material/{materialId}/ProductsProduced", nonWasteInputsDto);
        }

        public async Task<MaterialOutputsDto> GetMaterialOutputs(
            Guid id,
            Guid materialId)
        {
            return await Get<MaterialOutputsDto>($"{id}/Material/{materialId}/MaterialOutputs");
        }

        public async Task UpdateMaterialOutputs(
            Guid id,
            Guid materialId,
            MaterialOutputsDto materialOutputsDto)
        {
            await Put($"{id}/Material/{materialId}/MaterialOutputs", materialOutputsDto);
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
            return await this.Get<MaterialWasteOutputsDto>($"{id}/Material/{materialId}/MaterialWasteOutputs");
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
            await this.Put($"{id}/Material/{materialId}/MaterialWasteOutputs", materialWasteOutputsDto);
        }

        public async Task<bool?> GetReprocessedWasteLastYear(
            Guid id,
            Guid materialId)
        {
            return await Get<bool?>($"{id}/Material/{materialId}/WasteLastYear");
        }

        public async Task UpdateReprocessedWasteLastYear(
            Guid id,
            Guid materialId,
            ReprocessedWasteLastYear reprocessedWasteLastYear)
        {
            await Put($"{id}/Material/{materialId}/WasteLastYear", reprocessedWasteLastYear);
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
            return await Get<AccreditationMaterial>($"{id}/Material/{materialExternalId}");
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
            return await Get<List<string>>($"{id}/OverseasMaterial/{materialId}/WasteDescriptionCodes");
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
            await Post($"{id}/OverseasMaterial/{materialId}/WasteDescriptionCodes", wasteDecriptionCodes);
        }

        private string GetSiteName(
            SiteType siteType) => siteType == SiteType.Site ? "Material" : $"OverseasMaterial";
    }
}