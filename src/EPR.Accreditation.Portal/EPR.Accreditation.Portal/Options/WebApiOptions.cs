using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.Portal.Options;

[ExcludeFromCodeCoverage]
public class WebApiOptions
{
    public const string ConfigSection = "WebAPI";

    public string BaseEndpoint { get; set; }

    public string DownstreamScope { get; set; }
}
