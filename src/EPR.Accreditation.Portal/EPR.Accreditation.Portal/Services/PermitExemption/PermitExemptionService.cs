using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.PermitExemption.Interfaces;
using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.PermitExemption
{
    public class PermitExemptionService : IPermitExemptionService
    {

        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IHttpWastePermitService _httpWastePermitService;

        public PermitExemptionService(
            IHttpContextAccessor httpContextAccessor,
            IHttpWastePermitService httpWastePermitService)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _httpWastePermitService = httpWastePermitService ?? throw new ArgumentNullException(nameof(httpWastePermitService));
        }

        public async Task<PermitExemptionViewModel> GetPermitExemptionViewModel(Guid id)
        {
            return new PermitExemptionViewModel
            {
                Id = id,
                HasPermitExemption = await _httpWastePermitService.GetHasPermitExemption(id)
            };
        }
    }
}
