namespace EPR.Accreditation.App.Services.AuthServices.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}