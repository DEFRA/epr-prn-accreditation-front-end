namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;

    /// <summary>
    /// View model to represent an address used within the views
    /// </summary>
    public class AddressViewModel
    {
        /// <summary>
        /// Gets or sets address1.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(AddressResources), ErrorMessageResourceName = "AddressLine1Missing")]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets address2.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets town.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(AddressResources), ErrorMessageResourceName = "TownOrCityMisssing")]
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets county.
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// Gets or sets postcode.
        /// </summary>
        [RegularExpression(
            @"(?i)((A[BL]|B[ABDHLNRST]?|C[ABFHMORTVW]|D[ADEGHLNTY]|E[HNX]?|F[KY]|G[LUY]?|H[ADGPRSUX]|I[GMPV]|JE|K[ATWY]|L[ADELNSU]?|M[EKL]?|N[EGNPRW]?|O[LX]|P[AEHLOR]|R[GHM]|S[AEGKLMNOPRSTY]?|T[ADFNQRSW]|UB|W[ADFNRSV]|YO|ZE)[1-9]?[0-9]|([E|N|NW|SE|SW|W]1|EC[1-4]|WC[12])[A-HJKMNPR-Y]|[SW|W]([1-9][0-9]|[2-9])|EC[1-9][0-9]) [0-9][ABD-HJLNP-UW-Z]{2}",
            ErrorMessageResourceType = typeof(AddressResources),
            ErrorMessageResourceName = "PostCodeInvalid")]
        [Required(ErrorMessageResourceType = typeof(AddressResources), ErrorMessageResourceName = "PostCodeMissing")]
        public string Postcode { get; set; }
    }
}
