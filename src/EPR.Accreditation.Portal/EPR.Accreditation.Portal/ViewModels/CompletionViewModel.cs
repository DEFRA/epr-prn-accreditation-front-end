namespace EPR.Accreditation.Portal.ViewModels
{
    /// <summary>
    /// Completion view model.
    /// </summary>
    public class CompletionViewModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// SiteId.
        /// </summary>
        public Guid SiteId { get; set; }

        /// <summary>
        /// MaterialId.
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// CountryCode.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// AmountDue.
        /// </summary>
        public decimal AmountDue { get; set; }

        /// <summary>
        /// ReferenceNumber.
        /// </summary>
        public string ReferenceNumber { get; set; }
    }
}