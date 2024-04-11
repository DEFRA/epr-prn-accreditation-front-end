namespace EPR.Accreditation.Portal.ViewModels
{
    using EPR.Accreditation.Portal.Attributes.Validation;
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.ViewModels.Interfaces;

    /// <summary>
    /// View model to represent a row of data.
    /// </summary>
    public class NonWasteInputsRowViewModel : IEntryMade
    {
        /// <summary>
        /// Gets or sets the Type value for the current row.
        /// </summary>
        [RequiredIfOther("Tonnes", ErrorMessageResourceName = "TypeRequired", ErrorMessageResourceType = typeof(NonWasteInputLastYearResources))]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the Tonnes value for the current row.
        /// </summary>
        [RequiredIfOther("Type", ErrorMessageResourceName = "TonnesRequired", ErrorMessageResourceType = typeof(NonWasteInputLastYearResources))]
        public decimal? Tonnes { get; set; }

        /// <summary>
        /// Gets a value indicating whether an entry has been made in the row.
        /// </summary>
        public bool EntryMade => !string.IsNullOrWhiteSpace(this.Type) || this.Tonnes.HasValue;
    }
}
