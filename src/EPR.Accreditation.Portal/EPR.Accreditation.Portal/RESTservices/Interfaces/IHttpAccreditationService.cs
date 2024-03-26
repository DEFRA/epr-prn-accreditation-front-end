using EPR.Accreditation.Portal.DTOs;

namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpAccreditationService
    {
        Task CreateWastePermit(Guid accreditationId, DTOs.WastePermit wastePermit);

        Task<WastePermit> GetWastePermit(Guid accreditationId);
    }
}
