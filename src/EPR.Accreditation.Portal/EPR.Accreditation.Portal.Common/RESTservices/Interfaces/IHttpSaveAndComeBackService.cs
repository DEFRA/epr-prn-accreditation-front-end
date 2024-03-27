using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpSaveAndComeBackService
    {
        Task<SaveAndComeBack> GetSaveAndComeBack(Guid accreditationExternalId);

        Task AddSaveAndComeBack(
            Guid accreditationExternalId,
            SaveAndComeBack saveAndComeBack);

        Task DeleteSaveAndComeBack(Guid accreditationExternalId);
    }
}
