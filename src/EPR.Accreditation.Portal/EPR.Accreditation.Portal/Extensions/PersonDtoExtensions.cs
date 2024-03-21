using System.Diagnostics.CodeAnalysis;
using EPR.Accreditation.Portal.DTOs.UserAccount;

namespace EPR.Accreditation.Portal.Extensions;

[ExcludeFromCodeCoverage]
public static class PersonDtoExtensions
{
    public static string GetUserName(this PersonDto person)
    {
        return $"{person.FirstName} {person.LastName}";
    }
}
