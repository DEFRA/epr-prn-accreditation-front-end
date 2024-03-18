using EPR.Accreditation.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class WasteLicensesAndPermitsViewModel
    {

        [Required(ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "MissingRegistrationNumber")]
        [Range(0, 1000000, ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "RegistrationNumberNotInRange")]
        public double? RegistrationNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "MissingPermitNumber")]
        [Range(0, 1000000, ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "PermitNumberNotInRange")]
        public double? PermitNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "MissingActivityNumber")]
        [Range(0, 1000000, ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "ActivityNumberNotInRange")]
        public double? ActivityNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "MissingActivityReferenceNumber")]
        [Range(0, 1000000, ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "ActivityReferenceNumberNotInRange")]
        public double? ActivityReferenceNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "MissingDischargeConstentNumber")]
        [Range(0, 1000000, ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "DischargeConstentNumberNotInRange")]
        public double? DischargeConstentNumber { get; set; }
    }
}
