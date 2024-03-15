using EPR.Accreditation.App.ViewModels;

namespace EPR.Accreditation.App.Services.Interfaces
{
    public interface IAccreditationService
    {
        Task<ApplicationSavedViewModel> GetApplicationSavedViewModel(int id);
    }
}
