using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.Portal.Options;

[ExcludeFromCodeCoverage]
public class SessionOptions
{
    public const string ConfigSection = "Session";

    public int IdleTimeoutMinutes { get; set; }
}
