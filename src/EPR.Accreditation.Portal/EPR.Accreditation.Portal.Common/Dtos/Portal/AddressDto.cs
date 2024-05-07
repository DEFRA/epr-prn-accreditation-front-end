namespace EPR.Accreditation.Portal.Common.Dtos.Portal
{
    /// <summary>
    /// DTO class to represent an address
    /// </summary>
    public class AddressDto
    {
        /// <summary>
        /// Gets or sets the address line 1 value
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address line 2 value
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the town value
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets the county value
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the postcode value
        /// </summary>
        public string Postcode { get; set; }
    }
}
