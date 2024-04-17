namespace EPR.Accreditation.Portal.Options
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class AppSettingsConfigOptions
    {
        public const string ConfigSection = "AppSettings";

        /// <summary>
        /// Gets or sets the maximum number of rows for multi
        /// line records that can have extra rows added
        /// </summary>
        public int? MaximumMultiLineRecordNumber { get; set; }

        public int? DaysUntilExpiration { get; set; }
    }
}