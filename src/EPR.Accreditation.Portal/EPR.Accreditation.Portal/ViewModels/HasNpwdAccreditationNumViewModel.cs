namespace EPR.Accreditation.Portal.ViewModels
{
    /// <summary>
    /// Class for the view model that drives the view
    /// </summary>
    public class HasNpwdAccreditationNumViewModel
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
        public bool? Has2024NPWDAccreditation { get; set; }
    }
}
