namespace EPR.Accreditation.Portal.Services.AuthServices.Interfaces;

public interface ICorrelationIdProvider
{
    public Guid GetCurrentCorrelationIdOrNew();
}