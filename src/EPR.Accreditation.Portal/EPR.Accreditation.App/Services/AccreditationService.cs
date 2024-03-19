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
        }

        public ApplicationSavedViewModel GetApplicationSavedViewModel(Guid id)
        {
            return new ApplicationSavedViewModel
            {
                Id = id,
                ApplicationExpiry = (int)_configSettings.Value.Days_Until_Expiration
            };
        }
    }
}