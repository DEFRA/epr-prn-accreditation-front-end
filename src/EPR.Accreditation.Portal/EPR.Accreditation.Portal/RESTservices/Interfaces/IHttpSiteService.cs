using EPR.Accreditation.Portal.DTOs.Site;

namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpSiteService
    {
        Task<Site> GetSite(Guid accredititionId, Guid siteExternalId);

        Task<Guid> CreateSite(Guid accreditationExternalId, Site site);
    }
}

