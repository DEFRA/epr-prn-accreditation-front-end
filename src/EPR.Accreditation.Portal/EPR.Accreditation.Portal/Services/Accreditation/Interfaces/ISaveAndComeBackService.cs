namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    public interface ISaveAndComeBackService
    {
        Task AddSaveAndComeBack(
            Guid accreditationExternalId,
            RouteValueDictionary keyValuePairs);
    }
}