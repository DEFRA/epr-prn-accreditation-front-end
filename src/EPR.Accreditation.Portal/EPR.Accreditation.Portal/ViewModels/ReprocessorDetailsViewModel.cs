namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// Class for the view model that drives the view
    /// </summary>
    public class ReprocessorDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the Accreditation ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Overseas site ID
        /// </summary>
        public Guid OverseasSiteId { get; set; }

        /// <summary>
        /// Gets or sets the name of the company
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ReprocessorDetailsResources), ErrorMessageResourceName = "ErrorOrgName")]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of countries to populate the drop down in the view
        /// </summary>
        public IEnumerable<SelectListItem> Countries { get; set; }

        /// <summary>
        /// Gets or sets the ID of the country
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ReprocessorDetailsResources), ErrorMessageResourceName = "ErrorCountry")]
        public string CountryId { get; set; }

        /// <summary>
        /// Gets or sets the address of the overseas reprocessor
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ReprocessorDetailsResources), ErrorMessageResourceName = "ErrorAddress")]
        [MaxLength(500)]
        public string Address { get; set; }
    }
}
