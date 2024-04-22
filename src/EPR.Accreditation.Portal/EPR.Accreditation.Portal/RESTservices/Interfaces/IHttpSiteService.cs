using EPR.Accreditation.Portal.DTOs.Site;
using EPR.Accreditation.Portal.DTOs.WastePermit;

namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpSiteService
    {
        Task<Site> GetSite(Guid accredititionId, Guid siteExternalId);

        Task<Guid> CreateSite(Guid accreditationExternalId, Site site);

        Task UpdateSite(Guid id, Site site);
    }
}

