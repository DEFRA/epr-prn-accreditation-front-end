using AutoMapper;
using EPR.Accreditation.Portal.Options;
using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.Extensions.Options;

namespace EPR.Accreditation.Portal.Services.Accreditation
{
    public class AccreditationService : IAccreditationService
    {
        private readonly IMapper _mapper;

        protected IOptions<AppSettingsConfigOptions> _configSettings;

        protected IHttpAccreditationService _httpAccreditationService;

        public AccreditationService(IMapper mapper, IOptions<AppSettingsConfigOptions> configSettings, IHttpAccreditationService httpAccreditationService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _configSettings = configSettings ?? throw new ArgumentNullException(nameof(configSettings));
            if (_configSettings.Value?.DaysUntilExpiration == null)
                throw new ArgumentNullException(nameof(_configSettings.Value.DaysUntilExpiration));
            _httpAccreditationService = httpAccreditationService ?? throw new ArgumentNullException(nameof(httpAccreditationService));
        }

        public WasteLicensesAndPermitsViewModel GetWastePermitViewModel(Guid id)
        {
            var wastePermit = _httpAccreditationService.GetWastePermit(id);

            WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel = new WasteLicensesAndPermitsViewModel();
            
            if (wastePermit.Result != null)
            {
                wasteLicensesAndPermitsViewModel = _mapper.Map<WasteLicensesAndPermitsViewModel>(wastePermit.Result);
                wasteLicensesAndPermitsViewModel.Id = id;
            }
            
            return wasteLicensesAndPermitsViewModel;
        }

        public async Task SaveWastePermit(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel)
        {
            var wastePermit = _mapper.Map<DTOs.WastePermit>(wasteLicensesAndPermitsViewModel);
            
            await _httpAccreditationService.CreateWastePermit(wasteLicensesAndPermitsViewModel.Id, wastePermit);
        }
    }
}