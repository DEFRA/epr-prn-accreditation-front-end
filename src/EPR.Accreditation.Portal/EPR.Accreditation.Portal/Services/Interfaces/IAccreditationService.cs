using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Interfaces
{
    public interface IAccreditationService
    {
        ApplicationSavedViewModel GetApplicationSavedViewModel(Guid id);
    }
}
