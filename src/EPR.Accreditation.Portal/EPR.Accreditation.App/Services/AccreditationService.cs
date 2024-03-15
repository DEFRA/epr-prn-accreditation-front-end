using EPR.Accreditation.App.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace EPR.Accreditation.Portal.Services
{
    public class AccreditationService : IAccreditationService
    {
        protected IOptions<AppConfigSettings> _configSettings;

        public AccreditationService(IOptions<AppConfigSettings> configSettings)
        {
            _configSettings = configSettings ?? throw new ArgumentNullException(nameof(configSettings));
        }

        public async Task<ApplicationSavedViewModel> GetApplicationSavedViewModel(int id)
        {
            return new ApplicationSavedViewModel
            {
                Id = id,
                DateAndTime = DateTime.Now.ToString("dd/MM/yyyy hh:MM:ss"),
                ApplicationExpiry = _configSettings.Value.Application_Saved_Days_Amount
            };
        }
    }
}