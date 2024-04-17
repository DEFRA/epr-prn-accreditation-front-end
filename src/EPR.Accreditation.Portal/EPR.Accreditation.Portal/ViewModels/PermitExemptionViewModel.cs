namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;

    public class PermitExemptionViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(PermitExemptionResources), ErrorMessageResourceName = "ErrorMessage")]
        public bool? HasPermitExemption { get; set; }
    }
}