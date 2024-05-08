namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Attributes.Validation;
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.ViewModels.Interfaces;

    /// <summary>
    /// View model to represent a row of data for Type and Tonnes
    /// </summary>
    public class TypeTonnesRowViewModel : IEntryMade
    {
        /// <summary>
        /// Gets or sets the Type value for the current row.
        /// </summary>
        [RequiredIfOther("Tonnes", ErrorMessageResourceName = "TonnesRequired", ErrorMessageResourceType = typeof(NonWasteInputLastYearResources))]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessageResourceName = "OnlyLettersAllowed", ErrorMessageResourceType = typeof(NonWasteInputLastYearResources))]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the Tonnes value for the current row.
        /// </summary>
        [RequiredIfOther("Type", ErrorMessageResourceName = "TypeRequired", ErrorMessageResourceType = typeof(NonWasteInputLastYearResources))]
        [Range(0.001, 1000000.000, ErrorMessageResourceName = "TonnesNotWithinRange", ErrorMessageResourceType = typeof(NonWasteInputLastYearResources))]
        [NumericCharacterLength(11)]
        public decimal? Tonnes { get; set; }

        /// <summary>
        /// Gets a value indicating whether an entry has been made in the row.
        /// </summary>
        public bool EntryMade => !string.IsNullOrWhiteSpace(this.Type) || this.Tonnes.HasValue;
    }
}
