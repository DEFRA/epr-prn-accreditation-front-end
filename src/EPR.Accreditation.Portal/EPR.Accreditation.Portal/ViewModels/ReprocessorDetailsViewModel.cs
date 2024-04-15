namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;

    public class ReprocessorDetailsViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ReprocessorDetailsResources), ErrorMessageResourceName = "ErrorOrgName")]
        [StringLength(100)]
        public string OrganisationName { get; set; }

        public IEnumerable<string> Countries { get; set; }

        [Required(ErrorMessageResourceType = typeof(ReprocessorDetailsResources), ErrorMessageResourceName = "ErrorCountry")]
        public string SelectedCountry { get; set; }

        [Required(ErrorMessageResourceType = typeof(ReprocessorDetailsResources), ErrorMessageResourceName = "ErrorAddress")]
        [StringLength(500)]
        public string Address { get; set; }
    }
}
