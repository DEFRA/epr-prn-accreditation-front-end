using System.Diagnostics.CodeAnalysis;
using EPR.Accreditation.App.Services.AuthServices.Interfaces;

namespace EPR.Accreditation.App.Services.AuthServices;

[ExcludeFromCodeCoverage]
public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}