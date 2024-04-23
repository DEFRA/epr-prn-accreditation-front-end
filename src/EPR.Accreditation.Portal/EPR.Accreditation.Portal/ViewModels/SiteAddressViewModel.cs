namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.DTOs.Site;
    using EPR.Accreditation.Portal.Resources;

    /// <summary>
    /// SiteAddressViewModel.
    /// </summary>
    public class SiteAddressViewModel
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets siteId.
        /// </summary>
        public Guid SiteId { get; set; }

        /// <summary>
        /// Gets or sets materialId.
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// Gets or sets externalId.
        /// </summary>
        public Guid ExternalId { get; set; }

        /// <summary>
        /// Gets or sets address1.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(SiteAddressResources), ErrorMessageResourceName = "AddressLine1Missing")]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets address2.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets town.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(SiteAddressResources), ErrorMessageResourceName = "TownOrCityMisssing")]
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets county.
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// Gets or sets postcode.
        /// </summary>
        [RegularExpression(@"((A[BL]|B[ABDHLNRST]?|C[ABFHMORTVW]|D[ADEGHLNTY]|E[HNX]?|F[KY]|G[LUY]?|H[ADGPRSUX]|I[GMPV]|JE|K[ATWY]|L[ADELNSU]?|M[EKL]?|N[EGNPRW]?|O[LX]|P[AEHLOR]|R[GHM]|S[AEGKLMNOPRSTY]?|T[ADFNQRSW]|UB|W[ADFNRSV]|YO|ZE)[1-9]?[0-9]|([E|N|NW|SE|SW|W]1|EC[1-4]|WC[12])[A-HJKMNPR-Y]|[SW|W]([1-9][0-9]|[2-9])|EC[1-9][0-9]) [0-9][ABD-HJLNP-UW-Z]{2}")]
        [Required(ErrorMessageResourceType = typeof(SiteAddressResources), ErrorMessageResourceName = "PostCodeMissing")]
        public string Postcode { get; set; }

        /// <summary>
        /// Gets or sets organisationId.
        /// </summary>
        public Guid OrganisationId { get; set; }

        /// <summary>
        /// Gets or sets siteAuthorties.
        /// </summary>
        public IEnumerable<SiteAuthority> SiteAuthorties { get; set; }
    }
}
