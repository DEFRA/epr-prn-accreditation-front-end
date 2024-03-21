using System.Diagnostics.CodeAnalysis;

namespace EPR.Accreditation.Portal.DTOs.UserAccount;

[ExcludeFromCodeCoverage]
public class PersonDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string ContactEmail { get; set; }
}