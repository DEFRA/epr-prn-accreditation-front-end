using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    public interface ISiteService
    {
        Task<Site> GetSite(Guid id, Guid siteId);
    }
}
