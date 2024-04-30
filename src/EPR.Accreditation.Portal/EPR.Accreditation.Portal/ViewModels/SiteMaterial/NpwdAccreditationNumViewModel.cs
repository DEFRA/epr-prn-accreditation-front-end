namespace EPR.Accreditation.Portal.ViewModels.SiteMaterial
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;

    /// <summary>
    /// Class for the view model that drives the view
    /// </summary>
    public class NpwdAccreditationNumViewModel
    {
        /// <summary>
        /// Gets or sets the accreditation ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the material ID
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// Gets or sets whether or not NPWD number is present
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(NpwdAccrNumResources), ErrorMessageResourceName = "ErrorMessage")]
        [StringLength(12, MinimumLength = 9, ErrorMessageResourceType = typeof(NpwdAccrNumResources), ErrorMessageResourceName = "ErrorMessage")]
        [RegularExpression(@"^[A-Za-z]{2}\d{0,10}$", ErrorMessageResourceType = typeof(NpwdAccrNumResources), ErrorMessageResourceName = "ErrorMessageInvalidFormat")]
        public string AccreditationNumber { get; set; }
    }
}
