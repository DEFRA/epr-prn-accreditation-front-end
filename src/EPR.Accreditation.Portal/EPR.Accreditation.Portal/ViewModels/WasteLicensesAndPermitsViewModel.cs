using EPR.Accreditation.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class WasteLicensesAndPermitsViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "MissingRegistrationNumber")]
        [MaxLength(20, ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "RegistrationNumberNotInRange")]
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "MissingPermitNumber")]
        public string PermitNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "MissingActivityNumber")]
        public string ActivityNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "MissingActivityReferenceNumber")]
        public string ActivityReferenceNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "MissingDischargeConstentNumber")]
        public string DischargeConstentNumber { get; set; }
    }
}
