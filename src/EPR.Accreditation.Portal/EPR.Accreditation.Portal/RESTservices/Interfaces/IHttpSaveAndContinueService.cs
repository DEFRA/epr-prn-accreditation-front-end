using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpSaveAndContinueService
    {
        Task<ApplicationSavedViewModel> GetSaveAndContinue(Guid id);

        Task<bool> GetHasApplicationSaved(Guid id);

        Task AddSaveAndContinue(
            Guid id,
            ApplicationSavedViewModel viewModel);

        Task DeleteSaveAndContinue(Guid id);
    }
}
