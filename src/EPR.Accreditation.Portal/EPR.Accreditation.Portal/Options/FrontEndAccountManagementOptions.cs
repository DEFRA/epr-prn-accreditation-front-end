using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.Portal.Options;

[ExcludeFromCodeCoverage]
public class FrontEndAccountManagementOptions
{
    public const string ConfigSection = "FrontEndAccountManagement";

    public string BaseUrl { get; set; }
}