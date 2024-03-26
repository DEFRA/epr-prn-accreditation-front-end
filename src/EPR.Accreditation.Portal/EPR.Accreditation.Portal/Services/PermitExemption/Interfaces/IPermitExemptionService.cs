using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.PermitExemption.Interfaces
{
    public interface IPermitExemptionService
    {
        Task<PermitExemptionViewModel> GetPermitExemptionViewModel(Guid id);

        Task UpdatePermitExemption(PermitExemptionViewModel permitExemptionViewModel);
    }
}
