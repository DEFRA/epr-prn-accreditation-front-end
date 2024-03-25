using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.SaveAndContinue.Interfaces
{
    public interface ISaveAndContinueService
    {
        Task<ApplicationSavedViewModel> GetApplicationSavedViewModel(Guid id);

        Task AddSaveAndContinue(Guid id, ApplicationSavedViewModel viewModel);

        Task DeleteSaveAndContinue(Guid id);
    }
}
