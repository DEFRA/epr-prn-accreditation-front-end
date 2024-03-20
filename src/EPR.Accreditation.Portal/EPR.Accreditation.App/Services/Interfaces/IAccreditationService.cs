using EPR.Accreditation.App.ViewModels;

namespace EPR.Accreditation.App.Services.Interfaces
{
    public interface IAccreditationService
    {
        ApplicationSavedViewModel GetApplicationSavedViewModel(Guid id);

        Task<WasteLicensesAndPermitsViewModel> GetWasteLicensesAndPermitsViewModel(Guid id);

        Task<Task> SaveWasteLicensesAndPermitsViewMode(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewMode);
    }
}
