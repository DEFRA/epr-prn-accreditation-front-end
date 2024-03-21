using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.Portal.Options;

[ExcludeFromCodeCoverage]
public class GlobalVariables
{
    public string BasePath { get; set; }

    public bool UseLocalSession { get; set; }
}
