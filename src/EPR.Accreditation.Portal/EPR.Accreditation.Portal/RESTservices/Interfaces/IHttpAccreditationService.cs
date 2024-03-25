namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpAccreditationService
    {
        Task CreateAccreditation(
            Guid id,
            Guid siteId,
            DTOs.Accreditation accreditation);
    }
}
