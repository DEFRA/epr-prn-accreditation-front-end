using EPR.Accreditation.Portal.DTOs;

namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpAccreditationService
    {
        Task CreateAccreditation(
            Guid id,
            DTOs.Accreditation accreditation);

        Task CreateWastePermit(Guid accreditationId, DTOs.WastePermit wastePermit);

        Task<WastePermit> GetWastePermit(Guid accreditationId);
    }
}
