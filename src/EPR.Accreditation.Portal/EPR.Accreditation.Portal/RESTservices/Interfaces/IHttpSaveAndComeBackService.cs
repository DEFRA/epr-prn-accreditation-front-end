using EPR.Accreditation.Portal.DTOs.SaveAndComeBack;

namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpSaveAndComeBackService
    {
        Task<SaveAndComeBack> GetSaveAndComeBack(Guid accreditationExternalId);

        Task AddSaveAndComeBack(
            Guid accreditationExternalId,
            SaveAndComeBack saveAndContinue);

        Task DeleteSaveAndComeBack(Guid accreditationExternalId);
    }
}
