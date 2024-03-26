using EPR.Accreditation.Portal.DTOs;
using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    public interface IAccreditationService
    {
        WasteLicensesAndPermitsViewModel GetWasteLicensesAndPermitsViewModel(Guid id);

        Task SaveWastePermit(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel);
    }
}
