using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    public interface IWastePermitService
    {
        Task<PermitExemptionViewModel> GetPermitExemptionViewModel(Guid id);

        Task UpdatePermitExemption(PermitExemptionViewModel permitExemptionViewModel);
    }
}
