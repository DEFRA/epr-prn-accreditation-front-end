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
            var exemptionReferences = await _httpAccreditationSiteService.GetExemptionReferences(id, siteId);

            return new ExemptionReferencesViewModel
            {
                Id = id,
                SiteId = siteId,
                ReferenceNumber1 = exemptionReferences.ElementAt(0).ToString(),
                ReferenceNumber2 = exemptionReferences.ElementAt(1).ToString(),
                ReferenceNumber3 = exemptionReferences.ElementAt(2).ToString(),
                ReferenceNumber4 = exemptionReferences.ElementAt(3).ToString(),
                ReferenceNumber5 = exemptionReferences.ElementAt(4).ToString()
            };
        }

        public async Task UpdateExemptionReferences(ExemptionReferencesViewModel viewModel)
        {

            //var exemptionReferences = new List<ExemptionReference>
            //{
            //    viewModel.ReferenceNumber1,
            //    viewModel.ReferenceNumber2,
            //    viewModel.ReferenceNumber3,
            //    viewModel.ReferenceNumber4,
            //    viewModel.ReferenceNumber5
            //};

            //viewModel.ExemptionReferences = exemptionReferences;

            await _httpAccreditationSiteService.UpdateExemptionReferences(
                viewModel.Id,
                viewModel.SiteId,
                viewModel.ExemptionReferences
                );
        }
    }
}
