using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.App.Options;

[ExcludeFromCodeCoverage]
public class AppSettingsConfigOptions
{
    public const string ConfigSection = "AppSettings";

    public int? Days_Until_Expiration { get; set; }
}
