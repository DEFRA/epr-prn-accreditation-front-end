using EPR.Accreditation.API.Common.Dtos;
using EPR.Accreditation.API.Common.Enums;

namespace EPRN.Portal.RESTServices.Interfaces
{
    public interface IHttpAccreditationService
    {
        Task<int> CreateWastePermit(
            int Id,
            int AccreditationId);

        Task<WastePermit> GetWastePermit(int Id);

        Task<Accreditation> GetAccreditation (string Id);
    }
}
