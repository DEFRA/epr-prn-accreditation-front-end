using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.Portal.Options;

[ExcludeFromCodeCoverage]
public class EmailAddressOptions
{
    public const string ConfigSection = "EmailAddresses";

    public string DataProtection { get; set; }

    public string DefraGroupProtectionOfficer { get; set; }
}
