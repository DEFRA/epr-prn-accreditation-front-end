using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    public interface IAccreditationService
    {
        ApplicationSavedViewModel GetApplicationSavedViewModel(Guid id);

        Task<WasteLicensesAndPermitsViewModel> GetWasteLicensesAndPermitsViewModel(Guid id);

        Task<Task> SaveWasteLicensesAndPermitsViewMode(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewMode);

        Task SaveAccreditation(DTOs.Accreditation accreditation);
    }
}
