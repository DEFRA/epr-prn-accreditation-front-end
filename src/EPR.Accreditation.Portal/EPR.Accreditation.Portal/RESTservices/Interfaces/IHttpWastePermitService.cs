using EPR.Accreditation.Portal.DTOs.WastePermit;

namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpWastePermitService
    {
        Task<bool?> GetHasPermitExemption(Guid id);

        Task UpdatePermitExemption(Guid id, PermitExemption permitExemption);
    }
}
