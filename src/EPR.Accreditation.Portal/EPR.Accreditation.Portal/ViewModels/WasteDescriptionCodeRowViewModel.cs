namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.ViewModels.Interfaces;

    /// <summary>
    /// View model for a row of data in WasteDescriptionCodeViewModel
    /// </summary>
    public class WasteDescriptionCodeRowViewModel : IEntryMade
    {
        /// <summary>
        /// Gets or sets the waste description code as entered by the user
        /// </summary>
        [StringLength(50, ErrorMessageResourceName = "TextTooLong", ErrorMessageResourceType = typeof(WasteDescriptionCodeResources))]
        public string WasteDescriptionCode { get; set; }

        /// <summary>
        /// Gets a value indicating whether an entry has been made for this row of data
        /// </summary>
        public bool EntryMade => !string.IsNullOrWhiteSpace(WasteDescriptionCode);
    }
}
