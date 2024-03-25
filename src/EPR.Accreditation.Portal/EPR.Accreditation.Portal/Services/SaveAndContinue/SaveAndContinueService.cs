using EPR.Accreditation.Portal.Options;
using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.SaveAndContinue.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.Extensions.Options;

namespace EPR.Accreditation.Portal.Services.SaveAndContinue
{
    public class SaveAndContinueService : ISaveAndContinueService
    {
        protected IOptions<AppSettingsConfigOptions> _configSettings;
        protected readonly IHttpSaveAndContinueService _httpSaveAndContinueService;

        public SaveAndContinueService(IOptions<AppSettingsConfigOptions> configSettings)
        {
            _configSettings = configSettings ?? throw new ArgumentNullException(nameof(configSettings));
            if (_configSettings.Value?.DaysUntilExpiration == null)
                throw new ArgumentNullException(nameof(_configSettings.Value.DaysUntilExpiration));
        }

        public Task AddSaveAndContinue(Guid id, ApplicationSavedViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSaveAndContinue(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationSavedViewModel> GetApplicationSavedViewModel(Guid id)
        {
            var viewModel = await _httpSaveAndContinueService.GetApplicationSaved(id);
        }
    }
}
