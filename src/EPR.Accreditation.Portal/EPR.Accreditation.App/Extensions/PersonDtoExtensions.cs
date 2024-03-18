using System.Diagnostics.CodeAnalysis;
using EPR.Accreditation.App.DTOs.UserAccount;

namespace EPR.Accreditation.App.Extensions;

[ExcludeFromCodeCoverage]
public static class PersonDtoExtensions
{
    public static string GetUserName(this PersonDto person)
    {
        return $"{person.FirstName} {person.LastName}";
    }
}
