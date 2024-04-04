using AutoMapper;
using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation
{
    public class AccreditationSiteService : IAccreditationSiteService
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IHttpAccreditationSiteService _httpAccreditationSiteService;
        private readonly IMapper _mapper;

        public AccreditationSiteService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ExemptionReferencesViewModel> GetExemptionReferencesViewModel(
            Guid id,
            Guid siteId)
        {
            return new ExemptionReferencesViewModel
            {

            };
        }

        public async Task UpdateExemptionReferences(ExemptionReferencesViewModel viewModel)
        {

            var exemptionReferences = new List<string>
            {
                viewModel.ReferenceNumber1,
                viewModel.ReferenceNumber2,
                viewModel.ReferenceNumber3,
                viewModel.ReferenceNumber4,
                viewModel.ReferenceNumber5
            };

            viewModel.ExemptionReferences = exemptionReferences;

            await _httpAccreditationSiteService.UpdateExemptionReferences(
                viewModel.Id,
                viewModel.SiteId,
                viewModel.ExemptionReferences
                );
        }
    }
}
