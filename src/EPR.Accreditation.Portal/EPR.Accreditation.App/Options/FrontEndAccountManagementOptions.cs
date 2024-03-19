using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.App.Options;

[ExcludeFromCodeCoverage]
public class FrontEndAccountManagementOptions
{
    public const string ConfigSection = "FrontEndAccountManagement";

    public string BaseUrl { get; set; }
}