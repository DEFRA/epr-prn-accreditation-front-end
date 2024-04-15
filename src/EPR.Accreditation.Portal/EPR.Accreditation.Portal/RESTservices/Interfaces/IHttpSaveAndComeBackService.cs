namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    using EPR.Accreditation.Portal.DTOs.SaveAndComeBack;

    public interface IHttpSaveAndComeBackService
    {
        Task<SaveAndComeBack> GetSaveAndComeBack(Guid accreditationExternalId);

        Task AddSaveAndComeBack(
            Guid accreditationExternalId,
            SaveAndComeBack saveAndComeBack);

        Task DeleteSaveAndComeBack(Guid accreditationExternalId);
    }
}