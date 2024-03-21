using EPR.Accreditation.Portal.Options;
using EPR.Accreditation.Portal.Services.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.Extensions.Options;

namespace EPR.Accreditation.Portal.Services
{
    public class AccreditationService : IAccreditationService
    {
        protected IOptions<AppSettingsConfigOptions> _configSettings;

        public AccreditationService(IOptions<AppSettingsConfigOptions> configSettings)
        {
            _configSettings = configSettings ?? throw new ArgumentNullException(nameof(configSettings));
            if (_configSettings.Value?.DaysUntilExpiration == null)
                throw new ArgumentNullException(nameof(_configSettings.Value.DaysUntilExpiration));
        }

        public ApplicationSavedViewModel GetApplicationSavedViewModel(Guid id)
        {
            return new ApplicationSavedViewModel
            {
                Id = id,
                ApplicationExpiry = _configSettings.Value.DaysUntilExpiration.Value
            };
        }

        public async Task<WasteLicensesAndPermitsViewModel> GetWasteLicensesAndPermitsViewModel(Guid id)
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
    }
}