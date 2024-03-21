using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.Portal.Options;

[ExcludeFromCodeCoverage]
public class ValidationOptions
{
    public const string ConfigSection = "Validation";

    public int MaxIssuesToProcess { get; set; }

    public string MaxIssueReportSize { get; set; }
}