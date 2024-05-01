namespace EPR.Accreditation.Portal.Configuration
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Configuration class defining the section of content links from the app settings
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ContentLinksConfiguration
    {
        /// <summary>
        /// Gets or sets the name of the section to retrieve the config for
        /// </summary>
        public const string SectionName = "ContentLinks";

        /// <summary>
        /// Gets or sets the url for the main heading on the accreditation site
        /// </summary>
        public string MainHeadingLink { get; set; }

        /// <summary>
        /// Gets or sets the url for consolidated waste list
        /// </summary>
        public string ConsolidatedWasteList { get; set; }

        /// <summary>
        /// Gets or sets the URL for the National Packaging Waste Database
        /// </summary>
        public string NationalPackagingWasteDb { get; set; }
    }
}