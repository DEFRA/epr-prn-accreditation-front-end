using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.Portal.DTOs.UserAccount;

[ExcludeFromCodeCoverage]
public record AcceptNominationRequest
{
    public string Telephone { get; set; }

    public string NomineeDeclaration { get; set; }
}