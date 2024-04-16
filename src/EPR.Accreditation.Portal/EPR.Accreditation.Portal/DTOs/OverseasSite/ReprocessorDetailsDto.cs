namespace EPR.Accreditation.Portal.DTOs.OverseasSite
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;

    /// <summary>
    /// DTO to for the view model
    /// </summary>
    public class ReprocessorDetailsDto
    {
        /// <summary>
        /// Gets or sets the organisation name
        /// </summary>
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the selected country
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ReprocessorDetailsResources), ErrorMessageResourceName = "ErrorCountry")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the address
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ReprocessorDetailsResources), ErrorMessageResourceName = "ErrorAddress")]
        [MaxLength(500)]
        public string Address { get; set; }
    }
}
