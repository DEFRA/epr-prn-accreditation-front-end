using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    public interface IAccreditationSiteMaterialService
    {
        // available for both reprocessor and exporter
        Task<string> GetWasteName(
            Guid id,
            Guid? siteId,
            Guid materialId);

        // available for both reprocessor and exporter
        Task<WasteSourceViewModel> GetWasteSource(
            SiteType siteType,
            Guid id,
            Guid? siteId,
            Guid materialId);

        // available for both reprocessor and exporter
        Task UpdateWasteSource(
            SiteType siteType,
            WasteSourceViewModel wasteSourceViewModel);

        // only reprocessor
        Task<NonWasteInputsViewModel> GetNonWasteInputs(
            Guid id,
            Guid materialId);

        // only reprocessor
        Task UpdateNonWasteInputs(NonWasteInputsViewModel nonWasteInputsViewModel);

        // only reprocessor
        Task<MaterialOutputsViewModel> GetMaterialOutputs(
            Guid id,
            Guid materialId);

        // only reprocessor
        Task UpdateMaterialOutputs(MaterialOutputsViewModel materialOutputsViewModel);

        // only reprocessor
        Task<ReprocessedWasteLastYearViewModel> GetReprocessedWasteLastYearViewModel(
            Guid id,
            Guid materialId);

        Task UpdateReprocessedWasteLastYear(
            ReprocessedWasteLastYearViewModel reprocessedWasteLastYearViewModel);

        /// <summary>
        /// Returns annual waste (actual or estimated) data.
        /// </summary>
        /// <param name="id">Accreditation id.</param>
        /// <param name="materialId">Material id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MaterialWasteOutputsViewModel> GetMaterialWasteOutputs(Guid id, Guid materialId);

        /// <summary>
        /// Updates annual waste (actual or estimated) data.
        /// </summary>
        /// <param name="materialWasteOutputsViewModel"> Material waste output view model.</param>
        /// <returns>A <see cref="Task{TResult}"/> Nothing returned.</returns>
        Task UpdateMaterialWasteOutputs(MaterialWasteOutputsViewModel materialWasteOutputsViewModel);

    }
}
