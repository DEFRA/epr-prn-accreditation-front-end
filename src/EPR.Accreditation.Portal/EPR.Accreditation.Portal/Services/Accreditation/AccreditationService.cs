using EPR.Accreditation.Portal.DTOs;
using EPR.Accreditation.Portal.Options;
using EPR.Accreditation.Portal.RESTservices;
using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.Extensions.Options;

namespace EPR.Accreditation.Portal.Services.Accreditation
{
    public class AccreditationService : IAccreditationService
    {
        protected IOptions<AppSettingsConfigOptions> _configSettings;

        protected IHttpAccreditationService _httpAccreditationService;

        public AccreditationService(IOptions<AppSettingsConfigOptions> configSettings, IHttpAccreditationService httpAccreditationService)
        {
            _configSettings = configSettings ?? throw new ArgumentNullException(nameof(configSettings));
            if (_configSettings.Value?.DaysUntilExpiration == null)
                throw new ArgumentNullException(nameof(_configSettings.Value.DaysUntilExpiration));
            _httpAccreditationService = httpAccreditationService ?? throw new ArgumentNullException(nameof(httpAccreditationService));
        }

        public WasteLicensesAndPermitsViewModel GetWasteLicensesAndPermitsViewModel(Guid id)
        {
            var wastePermit = _httpAccreditationService.GetWastePermit(id);

            WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel = new WasteLicensesAndPermitsViewModel();

            if (wastePermit.Result != null)
            {
                wasteLicensesAndPermitsViewModel.Id = id;
                wasteLicensesAndPermitsViewModel.PermitNumber = wastePermit.Result.EnvironmentalPermitNumber;
                wasteLicensesAndPermitsViewModel.DischargeConstentNumber = wastePermit.Result.DischargeConsentNumber;
                wasteLicensesAndPermitsViewModel.RegistrationNumber = wastePermit.Result.DealerRegistrationNumber;
                wasteLicensesAndPermitsViewModel.ActivityReferenceNumber = wastePermit.Result.PartAActivityReferenceNumber;
                wasteLicensesAndPermitsViewModel.ActivityNumber = wastePermit.Result.PartBActivityReferenceNumber;
            }

            return wasteLicensesAndPermitsViewModel;
        }

        public async Task SaveWastePermit(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel)
        {
            var wastePermit = new DTOs.WastePermit();
            wastePermit.EnvironmentalPermitNumber = wasteLicensesAndPermitsViewModel.PermitNumber;
            wastePermit.DischargeConsentNumber = wasteLicensesAndPermitsViewModel.DischargeConstentNumber;
            wastePermit.DealerRegistrationNumber = wasteLicensesAndPermitsViewModel.RegistrationNumber;
            wastePermit.PartAActivityReferenceNumber = wasteLicensesAndPermitsViewModel.ActivityReferenceNumber;
            wastePermit.PartBActivityReferenceNumber = wasteLicensesAndPermitsViewModel.ActivityNumber;
         
            await _httpAccreditationService.CreateWastePermit(wasteLicensesAndPermitsViewModel.Id, wastePermit);
        }
    }
}