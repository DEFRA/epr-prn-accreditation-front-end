using AutoMapper;
using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation
{
    public class WastePermitService : IWastePermitService
    {

        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IHttpWastePermitService _httpWastePermitService;
        private readonly IMapper _mapper;

        public WastePermitService(
            IHttpContextAccessor httpContextAccessor,
            IHttpWastePermitService httpWastePermitService,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _httpWastePermitService = httpWastePermitService ?? throw new ArgumentNullException(nameof(httpWastePermitService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PermitExemptionViewModel> GetPermitExemptionViewModel(Guid id)
        {
            return new PermitExemptionViewModel
            {
                Id = id,
                HasPermitExemption = await _httpWastePermitService.GetHasPermitExemption(id)
            };
        }

        public async Task UpdatePermitExemption(PermitExemptionViewModel permitExemptionViewModel)
        {
            var permitExemptionDto = _mapper.Map<DTOs.WastePermit.PermitExemption>(permitExemptionViewModel);

            await _httpWastePermitService.UpdatePermitExemption(
                permitExemptionViewModel.Id,
                permitExemptionDto
                );
        }
    }
}
