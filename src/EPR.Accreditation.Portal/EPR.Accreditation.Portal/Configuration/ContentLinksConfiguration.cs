namespace EPR.Accreditation.Portal.Configuration
{
    /// <summary>
    /// Configuration class defining the section of content links from the app settings
    /// </summary>
    public class ContentLinksConfiguration
    {
        /// <summary>
        /// Gets or sets the name of the section to retrieve the config for
        /// </summary>
        public const string SectionName = "ContentLinks";

        /// <summary>
        /// Gets or sets the url for consolidated waste list
        /// </summary>
        public string ConsolidatedWasteList { get; set; }
    }
}