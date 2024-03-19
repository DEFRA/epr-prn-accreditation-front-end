using EPR.Accreditation.App.ViewModels;

namespace EPR.Accreditation.App.Services.Interfaces
{
    public interface IAccreditationService
    {
        ApplicationSavedViewModel GetApplicationSavedViewModel(Guid id);

        //Task<PermitExemptionViewModel> GetPermitExemptionViewModel(int id);
    }
}
