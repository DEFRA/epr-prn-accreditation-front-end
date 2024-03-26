using EPR.Accreditation.Portal.DTOs;
using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    public interface IAccreditationService
    {
        WasteLicensesAndPermitsViewModel GetWastePermitViewModel(Guid id);

        Task SaveWastePermit(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel);
    }
}
