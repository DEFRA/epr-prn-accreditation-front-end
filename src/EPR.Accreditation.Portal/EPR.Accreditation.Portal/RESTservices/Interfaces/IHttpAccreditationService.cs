namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpAccreditationService
    {
        Task CreateAccreditation(
            Guid id,
            DTOs.Accreditation accreditation);
    }
}
