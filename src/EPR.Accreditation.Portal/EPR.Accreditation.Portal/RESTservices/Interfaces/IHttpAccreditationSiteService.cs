using EPR.Accreditation.Portal.DTOs.AccreditationSite;

namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpAccreditationSiteService
    {
        Task<IEnumerable<ExemptionReference>> GetExemptionReferences(
            Guid id,
            Guid siteId);
    }
}