using EPR.Accreditation.Portal.DTOs.SaveAndComeBack;

namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    public interface ISaveAndComeBackService
    {
        Task<SaveAndComeBack> GetSaveAndComeBack(Guid accreditationExternalId);

        Task AddSaveAndComeBack(
            Guid accreditationExternalId,
            RouteValueDictionary keyValuePairs);

        Task DeleteSaveAndComeBack(Guid accreditationExternalId);
    }
}