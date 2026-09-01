namespace EPR.Accreditation.Portal.ViewModels
{
    /// <summary>
    /// Completion view model.
    /// </summary>
    public class CompletionViewModel
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets SiteId.
        /// </summary>
        public Guid SiteId { get; set; }

        /// <summary>
        /// Gets or sets MaterialId.
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// Gets or sets CountryCode.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets AmountDue.
        /// </summary>
        public decimal AmountDue { get; set; }

        /// <summary>
        /// Gets or sets ReferenceNumber.
        /// </summary>
        public string ReferenceNumber { get; set; }
    }
}