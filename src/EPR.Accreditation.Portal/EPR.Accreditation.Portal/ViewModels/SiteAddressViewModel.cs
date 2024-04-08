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

        [RegularExpression(@"^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([AZa-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9]?[A-Za-z])))) [0-9][A-Za-z]{2})$")]
        [Required(ErrorMessageResourceType = typeof(SiteAddressResources), ErrorMessageResourceName = "PostCodeMissing")]
        public string PostCode { get; set; }
    }
}
