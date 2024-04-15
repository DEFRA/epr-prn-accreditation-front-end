namespace EPR.Accreditation.Portal.DTOs.UserAccount
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public record AcceptNominationRequest
    {
        public string Telephone { get; set; }

        public string NomineeDeclaration { get; set; }
    }
}