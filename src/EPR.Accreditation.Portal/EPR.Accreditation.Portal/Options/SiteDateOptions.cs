using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.Portal.Options;

[ExcludeFromCodeCoverage]
public class SiteDateOptions
{
    public const string ConfigSection = "SiteDates";

    public DateTime PrivacyLastUpdated { get; set; }

    public string DateFormat { get; set; }
}