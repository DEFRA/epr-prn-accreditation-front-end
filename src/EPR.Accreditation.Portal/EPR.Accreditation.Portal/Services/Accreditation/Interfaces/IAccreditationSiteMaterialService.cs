using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    public interface IAccreditationSiteMaterialService
    {
        Task<string> GetWasteName(
            Guid id,
            Guid siteId,
            Guid materialId);

        Task<WasteSourceViewModel> GetWasteSource(
            SiteType siteType,
            Guid id,
            Guid siteId,
            Guid materialId);

        Task UpdateWasteSource(
            SiteType siteType,
            WasteSourceViewModel wasteSourceViewModel);

        Task<MaterialOutputsViewModel> GetMaterialOutputs(
            Guid id,
            Guid siteId,
            Guid materialId);

        Task UpdateMaterialOutputs(
            MaterialOutputsViewModel materialOutputsViewModel);

        Task<ReprocessedWasteLastYearViewModel> GetReprocessedWasteLastYearViewModel(
            Guid id,
            Guid siteId,
            Guid materialId);

        Task UpdateReprocessedWasteLastYear(
            ReprocessedWasteLastYearViewModel reprocessedWasteLastYearViewModel);
    }
}
