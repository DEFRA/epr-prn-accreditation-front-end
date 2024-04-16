namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ReprocessorDetailsViewModel
    {
        public Guid Id { get; set; }

        public Guid OverseasSiteId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ReprocessorDetailsResources), ErrorMessageResourceName = "ErrorOrgName")]
        [MaxLength(100)]
        public string Name { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        [Required(ErrorMessageResourceType = typeof(ReprocessorDetailsResources), ErrorMessageResourceName = "ErrorCountry")]
        public int CountryId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ReprocessorDetailsResources), ErrorMessageResourceName = "ErrorAddress")]
        [MaxLength(500)]
        public string Address { get; set; }
    }
}
