using EPR.Accreditation.Portal.Services.PermitExemption.Interfaces;
using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.PermitExemption
{
    public class PermitExemptionService : IPermitExemptionService
    {
        public async Task<PermitExemptionViewModel> GetPermitExemptionViewModel(Guid id)
        {
            return new PermitExemptionViewModel
            {
                Id = id,
                HasPermitExemption = true
            };
        }
    }
}
