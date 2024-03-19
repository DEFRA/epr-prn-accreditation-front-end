using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.App.Options;

[ExcludeFromCodeCoverage]
public class AzureAdB2COptions
{
    public const string ConfigSection = "AzureAdB2C";

    public string SignedOutCallbackPath { get; set; }
}