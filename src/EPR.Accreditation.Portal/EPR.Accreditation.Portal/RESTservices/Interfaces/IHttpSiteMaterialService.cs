using EPR.Accreditation.Portal.Common.Dtos.Portal;
using EPR.Accreditation.Portal.Enums;

namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpSiteMaterialService
    {
        Task<string> GetMeterialName(
            Guid id,
            Guid siteId,
            Guid materialId,
            Enums.Language language);

        Task<string> GetWasteSource(
            SiteType siteType,
            Guid id, 
            Guid siteId, 
            Guid materialId);

        Task UpdateWasteSource(
            SiteType siteType,
            Guid id,
            Guid siteId,
            Guid materialId,
            string wasteSource);

        Task<MaterialOutputsDto> GetMaterialOutputs(
            Guid id,
            Guid siteId,
            Guid materialId);

        Task UpdateMaterialOutputs(
            Guid id,
            Guid siteId,
            Guid materialId,
            MaterialOutputsDto materialOutputsDto);
    }
}
