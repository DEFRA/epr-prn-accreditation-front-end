namespace EPR.Accreditation.Portal.Configuration
{
    /// <summary>
    /// Represents configuration for the section marked "Services"
    /// </summary>
    public class ServicesConfiguration
    {
        /// <summary>
        /// The name to look for in the config file
        /// </summary>
        public const string SectionName = "Services";

        /// <summary>
        /// Gets or sets the Service configuration as found in the config
        /// </summary>
        public Service AccreditationFacade { get; set; }
    }
}
