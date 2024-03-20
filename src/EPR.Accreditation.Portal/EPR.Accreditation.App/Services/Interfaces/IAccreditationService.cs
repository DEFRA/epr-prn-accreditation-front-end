using EPR.Accreditation.App.ViewModels;

namespace EPR.Accreditation.App.Services.Interfaces
{
    public interface IAccreditationService
    {
        ApplicationSavedViewModel GetApplicationSavedViewModel(Guid id);

        WasteLicensesAndPermitsViewModel GetWasteLicensesAndPermitsViewModel(Guid id);

        Task SaveWasteLicensesAndPermits(WasteLicensesAndPermitsViewModel viewModel);
    }
}
