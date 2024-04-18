namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;

    public class HasOverseasAgentViewModel
    {
        public Guid ExternalId { get; set; }

        [Required(ErrorMessageResourceName = "MissingSelectionErrorMessage", ErrorMessageResourceType = typeof(HasOverseasAgentResources))]
        public bool? UseOverseasAgent { get; set; }

    }
}
