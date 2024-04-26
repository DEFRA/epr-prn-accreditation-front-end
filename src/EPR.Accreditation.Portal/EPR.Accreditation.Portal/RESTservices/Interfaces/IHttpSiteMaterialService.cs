namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.DTOs.MaterialReprocessorDetails;
    using EPR.Accreditation.Portal.DTOs.SiteMaterial;
    using EPR.Accreditation.Portal.Enums;

    public interface IHttpSiteMaterialService
    {
        // available for both reprocessor and exporter
        Task<string> GetMeterialName(
            SiteType siteType,
            Guid id,
            Guid materialId,
            Enums.Language language);

        // available for both reprocessor and exporter
        Task<string> GetWasteSource(
            SiteType siteType,
            Guid id,
            Guid? siteId,
            Guid materialId);

        // available for both reprocessor and exporter
        Task UpdateWasteSource(
            SiteType siteType,
            Guid id,
            Guid? siteId,
            Guid materialId,
            string wasteSource);

        // only reprocessor
        Task<ReprocessingSupportingInformationDto> GetNonWasteInputs(
            Guid id,
            Guid materialId);

        // only reprocessor
        Task UpdateNonWasteInputs(
            Guid id,
            Guid materialId,
            ReprocessingSupportingInformationDto nonWasteInputsDto);

        Task<ReprocessingSupportingInformationDto> GetProductsProduced(
            Guid id,
            Guid materialId);

        Task UpdateProductsProduced(
            Guid id,
            Guid materialId,
            ReprocessingSupportingInformationDto nonWasteInputsDto);

        // only reprocessor
        Task<MaterialOutputsDto> GetMaterialOutputs(
            Guid id,
            Guid materialId);

        // only reprocessor
        Task UpdateMaterialOutputs(
            Guid id,
            Guid materialId,
            MaterialOutputsDto materialOutputsDto);

        /// <summary>
        /// Gets material waste input.
        /// </summary>
        /// <param name="id">Accreditation id.</param>
        /// <param name="materialId">Material id.</param>
        /// <returns>Material waste output dto.</returns>
        Task<MaterialWasteInputsDto> GetMaterialWasteInputs(
            Guid id,
            Guid materialId);

        /// <summary>
        /// Updates material waste output.
        /// </summary>
        /// <param name="id">Accreditation id.</param>
        /// <param name="materialId">Material id.</param>
        /// <param name="materialWasteOutputsDto">Material waste output dto.</param>
        /// <returns>Nothing.</returns>
        Task UpdateMaterialWasteInputs(
            Guid id,
            Guid materialId,
            MaterialWasteInputsDto materialWasteOutputsDto);

        // only reprocessor
        Task<bool?> GetReprocessedWasteLastYear(
            Guid id,
            Guid materialId);

        // only reprocessor
        Task UpdateReprocessedWasteLastYear(
            Guid id,
            Guid materialId,
            ReprocessedWasteLastYear reprocessedWasteLastYear);

        /// <summary>
        /// Gets the waste description codes for the accreditation material
        /// </summary>
        /// <param name="id">The accreditation id</param>
        /// <param name="siteId">The id of the overseas site</param>
        /// <param name="materialId">The material id</param>
        /// <returns>The waste description code dto</returns>
        Task<List<string>> GetWasteDescriptionCodes(
            Guid id,
            Guid siteId,
            Guid materialId);

        /// <summary>
        /// Calls the Facade to save the waste description codes for the accreditation
        /// material
        /// </summary>
        /// <param name="id">The id of the accreditation application</param>
        /// <param name="siteId">The id of the overseas site</param>
        /// <param name="materialId">The id of the material</param>
        /// <param name="wasteDecriptionCodes">List of the waste description codes to save</param>
        /// <returns>Async task</returns>
        Task SaveWasteDescriptionCodes(
            Guid id,
            Guid siteId,
            Guid materialId,
            IEnumerable<string> wasteDecriptionCodes);

        /// <summary>
        /// Gets whether a 2024 NPWD Accreditation number is present
        /// </summary>
        /// <param name="id">Accreditation ID</param>
        /// <param name="materialId">Material ID</param>
        /// <returns>A boolean value</returns>
        Task<bool?> GetHasNpwdAccreditationNumber(
            Guid id,
            Guid materialId);

        /// <summary>
        /// Updates the 2024 NPWD Accreditation Number
        /// </summary>
        /// <param name="id">Accreditation ID</param>
        /// <param name="materialId">Material ID</param>
        /// <param name="npwdAccreditationNumber">The boolean value within the DTO</param>
        /// <returns>Task completed asynchronously</returns>
        Task UpdateHasNpwdAccreditationNumber(
            Guid id,
            Guid materialId,
            NpwdAccreditationNumber npwdAccreditationNumber);
    }
}
