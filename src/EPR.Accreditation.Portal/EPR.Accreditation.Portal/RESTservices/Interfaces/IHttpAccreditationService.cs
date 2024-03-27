using EPR.Accreditation.Portal.DTOs.WastePermit;

namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpAccreditationService
    {
        Task CreateWastePermit(Guid accreditationId, LicensesAndPermitsReferences wastePermit);

        Task<LicensesAndPermitsReferences> GetWastePermit(Guid accreditationId);
    }
}
