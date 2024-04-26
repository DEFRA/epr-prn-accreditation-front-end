namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.ViewModels;
    using EPR.Accreditation.Portal.ViewModels.SiteMaterial;

    public interface IAccreditationSiteMaterialService
    {
        // available for both reprocessor and exporter
        Task<string> GetWasteName(
            SiteType siteType,
            Guid id,
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
        Task<ProductsProducedViewModel> GetProductsProduced(
            Guid id,
            Guid materialId);

        // only reprocessor
        Task UpdateProductsProduced(ProductsProducedViewModel nonWasteInputsViewModel);

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

        /// <summary>
        /// Gets the view model and any relevant data fore the waste description code page
        /// </summary>
        /// <param name="id">The id of the accreditation</param>
        /// <param name="overseasSiteId">The id of the overseas site</param>
        /// <param name="materialId">The id of the material that the waste codes are for</param>
        /// <returns>An async WasteDescriptionCodeViewModel</returns>
        Task<WasteDescriptionCodeViewModel> GetWasteDescriptionCodeViewModel(
            Guid id,
            Guid overseasSiteId,
            Guid materialId);

        /// <summary>
        /// Updates the waste description codes for the material associated to the accreditation
        /// </summary>
        /// <param name="wasteDescriptionCodeViewModel">The view model posted from the view</param>
        /// <returns>Async task</returns>
        Task UpdateWasteDescriptionCodeViewModel(
            WasteDescriptionCodeViewModel wasteDescriptionCodeViewModel);
    }
}
