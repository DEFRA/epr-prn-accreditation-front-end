namespace EPR.Accreditation.Portal.Services.Accreditation
{
    using AutoMapper;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;

    public class WastePermitService : IWastePermitService
    {
        private readonly IHttpWastePermitService _httpWastePermitService;
        private readonly IMapper _mapper;

        public WastePermitService(
            IHttpWastePermitService httpWastePermitService,
            IMapper mapper)
        {
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
                permitExemptionDto);
        }
    }
}
