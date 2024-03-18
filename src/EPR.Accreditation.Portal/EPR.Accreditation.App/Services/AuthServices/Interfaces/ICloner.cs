namespace EPR.Accreditation.App.Services.AuthServices.Interfaces;

public interface ICloner
{
    T Clone<T>(T source);
}