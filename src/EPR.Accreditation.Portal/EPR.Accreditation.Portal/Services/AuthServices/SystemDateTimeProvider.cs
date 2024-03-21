using System.Diagnostics.CodeAnalysis;
using EPR.Accreditation.Portal.Services.AuthServices.Interfaces;

namespace EPR.Accreditation.Portal.Services.AuthServices;

[ExcludeFromCodeCoverage]
public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}