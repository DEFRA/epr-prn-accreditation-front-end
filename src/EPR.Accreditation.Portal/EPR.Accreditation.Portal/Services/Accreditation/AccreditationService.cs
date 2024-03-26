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
            return new WasteLicensesAndPermitsViewModel
            {
                Id = id,
            };
        }

        public async Task<Task> SaveWasteLicensesAndPermitsViewMode(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewMode)
        {
            return Task.CompletedTask;
        }

        public async Task SaveAccreditation(Guid id, DTOs.Accreditation accreditation)
        {
            await _httpAccreditationService.CreateAccreditation(id, accreditation);
        }
    }
}