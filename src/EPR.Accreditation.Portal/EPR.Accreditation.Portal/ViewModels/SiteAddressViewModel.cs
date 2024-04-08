using EPR.Accreditation.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class SiteAddressViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(SiteAddressResources), ErrorMessageResourceName = "AddressLine1Missing")]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(SiteAddressResources), ErrorMessageResourceName = "TownOrCityMisssing")]
        public string TownOrCity { get; set; }

        public string County { get; set; }

        [RegularExpression(@"((A[BL]|B[ABDHLNRST]?|C[ABFHMORTVW]|D[ADEGHLNTY]|E[HNX]?|F[KY]|G[LUY]?|H[ADGPRSUX]|I[GMPV]|JE|K[ATWY]|L[ADELNSU]?|M[EKL]?|N[EGNPRW]?|O[LX]|P[AEHLOR]|R[GHM]|S[AEGKLMNOPRSTY]?|T[ADFNQRSW]|UB|W[ADFNRSV]|YO|ZE)[1-9]?[0-9]|([E|N|NW|SE|SW|W]1|EC[1-4]|WC[12])[A-HJKMNPR-Y]|[SW|W]([1-9][0-9]|[2-9])|EC[1-9][0-9]) [0-9][ABD-HJLNP-UW-Z]{2}")]
        [Required(ErrorMessageResourceType = typeof(SiteAddressResources), ErrorMessageResourceName = "PostCodeMissing")]
        public string PostCode { get; set; }
    }
}
