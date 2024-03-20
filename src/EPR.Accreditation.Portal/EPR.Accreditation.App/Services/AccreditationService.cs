using EPR.Accreditation.App.Options;
using EPR.Accreditation.App.Services.Interfaces;
using EPR.Accreditation.App.ViewModels;
using Microsoft.Extensions.Options;

namespace EPR.Accreditation.App.Services
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

        public WasteLicensesAndPermitsViewModel GetWasteLicensesAndPermitsViewModel(Guid id)
        {
            return new WasteLicensesAndPermitsViewModel
            {
                Id = id,
            };
        }

        public Task SaveWasteLicensesAndPermits(WasteLicensesAndPermitsViewModel viewModel)
        {
            return Task.CompletedTask;
        }
    }
}