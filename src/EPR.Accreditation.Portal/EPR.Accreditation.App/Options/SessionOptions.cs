using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.App.Options;

[ExcludeFromCodeCoverage]
public class SessionOptions
{
    public const string ConfigSection = "Session";

    public int IdleTimeoutMinutes { get; set; }
}
