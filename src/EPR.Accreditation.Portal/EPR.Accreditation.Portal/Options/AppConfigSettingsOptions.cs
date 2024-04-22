using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.Portal.Options;

[ExcludeFromCodeCoverage]
public class AppSettingsConfigOptions
{
    public const string ConfigSection = "AppSettings";

    public int? DaysUntilExpiration { get; set; }
}
