using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation
{
    public class AccreditationSiteService : IAccreditationSiteService
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IHttpAccreditationSiteService _httpAccreditationSiteService;

        public AccreditationSiteService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<ExemptionReferencesViewModel> GetExemptionReferencesViewModel(
            Guid id,
            Guid siteId)
        {
            return new ExemptionReferencesViewModel
            {

            };
        }
    }
}
