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

            return new WasteLicensesAndPermitsViewModel
            {
                Id = id,
                PermitNumber = wastePermit.Result.EnvironmentalPermitNumber,
                DischargeConstentNumber = wastePermit.Result.DischargeConsentNumber,
                RegistrationNumber = wastePermit.Result.DealerRegistrationNumber,
                ActivityReferenceNumber = wastePermit.Result.PartAActivityReferenceNumber,
                ActivityNumber = wastePermit.Result.PartBActivityReferenceNumber
            };
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