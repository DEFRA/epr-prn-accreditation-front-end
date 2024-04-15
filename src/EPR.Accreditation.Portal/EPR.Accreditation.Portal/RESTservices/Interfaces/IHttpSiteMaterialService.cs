using EPR.Accreditation.Portal.Common.Dtos.Portal;
using EPR.Accreditation.Portal.DTOs.MaterialReprocessorDetails;
using EPR.Accreditation.Portal.Enums;

namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpSiteMaterialService
    {
        // available for both reprocessor and exporter
        Task<string> GetMeterialName(
            Guid id,
            Guid? siteId,
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
        Task<NonWasteInputsDto> GetNonWasteInputs(
            Guid id,
            Guid materialId);

        // only reprocessor
        Task UpdateNonWasteInputs(
            Guid id,
            Guid materialId,
            NonWasteInputsDto nonWasteInputsDto);

        Task<NonWasteInputsDto> GetProductsProduced(
            Guid id,
            Guid materialId);

        Task UpdateProductsProduced(
            Guid id,
            Guid materialId,
            NonWasteInputsDto nonWasteInputsDto);

        // only reprocessor
        Task<MaterialOutputsDto> GetMaterialOutputs(
            Guid id,
            Guid materialId);

        // only reprocessor
        Task UpdateMaterialOutputs(
            Guid id,
            Guid materialId,
            MaterialOutputsDto materialOutputsDto);

        // only reprocessor
        Task<bool?> GetReprocessedWasteLastYear(
            Guid id,
            Guid materialId);

        // only reprocessor
        Task UpdateReprocessedWasteLastYear(
            Guid id,
            Guid materialId,
            ReprocessedWasteLastYear reprocessedWasteLastYear);
    }
}
