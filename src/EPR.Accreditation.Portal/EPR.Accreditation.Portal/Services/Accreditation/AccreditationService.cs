using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation
{
    public class AccreditationService : IAccreditationService
    {
        protected readonly IHttpAccreditationService _httpAccreditationService;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public AccreditationService(IHttpAccreditationService httpAccreditationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _httpAccreditationService = httpAccreditationService ?? throw new ArgumentNullException(nameof(httpAccreditationService));

        }

        public async Task<OperatorTypeViewModel> GetOperatorType(Guid id)
        {
            var result = await _httpAccreditationService.GetOperatorType(id);
            return new OperatorTypeViewModel { ExternalId = id, OperatorType = result };
        }

        public async Task<Guid> CreateAccreditation(OperatorTypeViewModel viewModel)
        {
            var accreditation = new Facade.Common.Dtos.Accreditation { OperatorTypeId = viewModel.OperatorType.Value };
            var externalId = await _httpAccreditationService.CreateAccreditation(accreditation);
            return externalId;
        }
    }
}