namespace EPR.Accreditation.Portal.ViewModels
{
    /// <summary>
    /// ViewModel for the waste description code pager
    /// </summary>
    public class WasteDescriptionCodeViewModel : BaseMultiRowView<WasteDescriptionCodeRowViewModel>
    {
        /// <summary>
        /// Gets or sets the id of the accreditation. Populated from the Route
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the id of the overseas site Id. Populated from the Route
        /// </summary>
        public Guid SiteId { get; set; }

        /// <summary>
        /// Gets or sets the material id that the waste codes are related to
        /// </summary>
        public Guid MaterialId { get; set; }
    }
}