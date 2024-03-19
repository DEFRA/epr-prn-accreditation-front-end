using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.App.DTOs.UserAccount
{
    [ExcludeFromCodeCoverage]
    public class DelegatedPersonNominatorDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string OrganisationName { get; set; }
    }
}
