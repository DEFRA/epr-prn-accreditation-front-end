using EPR.Accreditation.App.Services.Interfaces;
using EPR.Accreditation.App.ViewModels;
using Microsoft.Extensions.Configuration;

namespace EPR.Accreditation.App.Services
{
    public class AccreditationService : IAccreditationService
    {
        private readonly IConfiguration _configSettings;

        public AccreditationService(IConfiguration configSettings)
        {
            _configSettings = configSettings ?? throw new ArgumentNullException(nameof(configSettings));
        }

        public ApplicationSavedViewModel GetApplicationSavedViewModel(int id)
        {
            return new ApplicationSavedViewModel
            {
                Id = id,
                ApplicationExpiry = int.Parse(_configSettings["Days_Until_Expiration"])
            };
        }
    }
}