using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    public interface IAccreditationService
    {
        WasteLicensesAndPermitsViewModel GetWasteLicensesAndPermitsViewModel(Guid id);

        Task<Task> SaveWasteLicensesAndPermitsViewMode(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewMode);

        Task SaveAccreditation(Guid id, DTOs.Accreditation accreditation);
    }
}
