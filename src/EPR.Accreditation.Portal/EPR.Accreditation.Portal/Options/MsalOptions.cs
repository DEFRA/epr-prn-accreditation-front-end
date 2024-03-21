using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.Portal.Options;

[ExcludeFromCodeCoverage]
public class MsalOptions
{
    public const string ConfigSection = "MSAL";

    public bool DisableL1Cache { get; set; }

    public int L2SlidingExpiration { get; set; }
}