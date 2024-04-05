using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpSiteService
    {
        Task<Site> GetSite(Guid siteId);
    }
}

