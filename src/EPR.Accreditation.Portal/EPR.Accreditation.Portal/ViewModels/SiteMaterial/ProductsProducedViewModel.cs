namespace EPR.Accreditation.Portal.ViewModels.SiteMaterial
{
    /// <summary>
    /// View model for products produced (Last year and estimated)
    /// </summary>
    public class ProductsProducedViewModel : BaseMultiRowView<TypeTonnesRowViewModel>
    {
        /// <summary>
        /// Gets or sets the Id of the accreditation populated from the route values.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Id for the current material populated from the route values.
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether waste was processed last year.
        /// This is required so that we know which view to return.
        /// </summary>
        public bool? WasteLastYear { get; set; }
    }
}
