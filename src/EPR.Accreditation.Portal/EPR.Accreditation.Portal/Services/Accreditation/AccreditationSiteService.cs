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
            IHttpAccreditationSiteService httpAccreditationSiteService,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _httpAccreditationSiteService = httpAccreditationSiteService ?? throw new ArgumentNullException(nameof(httpAccreditationSiteService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ExemptionReferencesViewModel> GetExemptionReferencesViewModel(Guid id)
        {
            var exemptionReferences = await _httpAccreditationSiteService.GetExemptionReferences(id);

            return new ExemptionReferencesViewModel
            {
                Id = id,
                ExemptionReferencesVm = exemptionReferences.Select(x => new ExemptionReferenceViewModel
                {
                    Reference = x
                }).ToList()
            };
        }

        public async Task UpdateExemptionReferences(ExemptionReferencesViewModel viewModel)
        {
            var exemptionReferences = viewModel.ExemptionReferencesVm.Select(x => x.Reference);

            await _httpAccreditationSiteService.UpdateExemptionReferences(
                viewModel.Id,
                exemptionReferences
                );
        }
    }
}
