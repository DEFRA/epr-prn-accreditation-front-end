namespace EPR.Accreditation.Portal.Options
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Class the contains the main properties for the
    /// AppSettings section of the config file
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AppSettingsConfigOptions
    {
        public const string ConfigSection = "AppSettings";

        /// <summary>
        /// Gets or sets the value indicating the number of
        /// initial rows to display for views with type and
        /// tonnes
        /// </summary>
        public int? InitialTypeTonnesRows { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the number of
        /// initial rows to display for waste codes
        /// </summary>
        public int? InitialWasteCodesRows { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of rows for multi
        /// line records that can have extra rows added
        /// </summary>
        public int? MaximumMultiLineRecordNumber { get; set; }

        public int? DaysUntilExpiration { get; set; }
    }
}