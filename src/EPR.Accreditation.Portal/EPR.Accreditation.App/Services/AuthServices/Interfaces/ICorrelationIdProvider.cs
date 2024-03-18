namespace EPR.Accreditation.App.Services.AuthServices.Interfaces;

public interface ICorrelationIdProvider
{
    public Guid GetCurrentCorrelationIdOrNew();
}