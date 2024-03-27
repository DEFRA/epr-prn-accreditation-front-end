using EPR.Accreditation.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class WasteLicensesAndPermitsViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "MissingDealerRegistrationNumberNumber")]
        [MaxLength(20, ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "DealerRegistrationNumberNumberNotInRange")]
        public string DealerRegistrationNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "MissingEnvironmentalPermitNumber")]
        public string EnvironmentalPermitNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "MissingPartBActivityReferenceNumber")]
        public string PartBActivityReferenceNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "MissingPartAActivityReferenceNumber")]
        public string PartAActivityReferenceNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "MissingDischargeConstentNumber")]
        public string DischargeConsentNumber { get; set; }
    }
}
