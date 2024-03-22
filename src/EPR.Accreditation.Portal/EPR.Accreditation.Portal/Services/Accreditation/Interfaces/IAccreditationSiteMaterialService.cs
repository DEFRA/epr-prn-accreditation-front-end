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
            Guid id,
            Guid siteId,
            Guid materialId);

        Task UpdateWasteSource(WasteSourceViewModel wasteSourceViewModel);
    }
}
