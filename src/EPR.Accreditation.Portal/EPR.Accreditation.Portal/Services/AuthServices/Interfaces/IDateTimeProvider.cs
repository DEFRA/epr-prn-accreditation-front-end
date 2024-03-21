namespace EPR.Accreditation.Portal.Services.AuthServices.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}