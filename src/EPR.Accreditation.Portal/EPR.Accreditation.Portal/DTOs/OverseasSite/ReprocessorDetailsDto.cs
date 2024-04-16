namespace EPR.Accreditation.Portal.DTOs.OverseasSite
{
    using EPR.Accreditation.Portal.Resources;
    using System.ComponentModel.DataAnnotations;

    public class ReprocessorDetailsDto
    {
        [MaxLength(100)]
        public string OrganisationName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ReprocessorDetailsResources), ErrorMessageResourceName = "ErrorCountry")]
        public string SelectedCountry { get; set; }

        [Required(ErrorMessageResourceType = typeof(ReprocessorDetailsResources), ErrorMessageResourceName = "ErrorAddress")]
        [MaxLength(500)]
        public string Address { get; set; }
    }
}
