namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.ViewModels;
    using EPR.Accreditation.Portal.ViewModels.SiteMaterial;

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
    }
}
