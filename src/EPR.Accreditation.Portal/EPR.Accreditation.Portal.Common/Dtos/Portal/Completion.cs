namespace EPR.Accreditation.Portal.Common.Dtos.Portal
{
    /// <summary>
    /// Completion DTO class.
    /// </summary>
    public class Completion
    {
        /// <summary>
        /// Gets or sets the accrediation fee.
        /// </summary>
        public decimal AccreditationFee { get; set; }

        /// <summary>
        /// Gets or sets the reference number.
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        public string CountryCode { get; set; }
    }
}