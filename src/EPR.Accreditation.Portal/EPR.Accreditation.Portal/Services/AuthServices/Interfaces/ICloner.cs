namespace EPR.Accreditation.Portal.Services.AuthServices.Interfaces;

public interface ICloner
{
    T Clone<T>(T source);
}