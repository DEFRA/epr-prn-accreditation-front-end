namespace EPR.Accreditation.Portal.DTOs.Site
{
    /// <summary>
    /// Site DTO.
    /// </summary>
    public class Site
    {
        /// <summary>
        /// Gets or sets Address1.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets Address2.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets Town.
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets County.
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// Gets or sets Postcode.
        /// </summary>
        public string Postcode { get; set; }

        /// <summary>
        /// Gets or sets OrganisationId.
        /// </summary>
        public Guid OrganisationId { get; set; }
    }
}
