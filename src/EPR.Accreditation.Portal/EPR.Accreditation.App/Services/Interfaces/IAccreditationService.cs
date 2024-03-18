using EPR.Accreditation.App.ViewModels;

namespace EPR.Accreditation.App.Services.Interfaces
{
    public interface IAccreditationService
    {
        ApplicationSavedViewModel GetApplicationSavedViewModel(int id);
    }
}
