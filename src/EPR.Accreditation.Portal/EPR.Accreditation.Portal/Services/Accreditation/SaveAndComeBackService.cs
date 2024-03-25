using Azure.Core;
using EPR.Accreditation.Portal.DTOs.SaveAndComeBack;
using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using Newtonsoft.Json;

namespace EPR.Accreditation.Portal.Services.Accreditation
{
    public class SaveAndComeBackService : ISaveAndComeBackService
    {
        protected IHttpSaveAndComeBackService _httpSaveAndComeBackService;

        public SaveAndComeBackService(IHttpSaveAndComeBackService httpSaveAndComeBackService)
        {
            _httpSaveAndComeBackService = httpSaveAndComeBackService ?? throw new ArgumentNullException(nameof(httpSaveAndComeBackService));
        }

        public async Task<SaveAndComeBack> GetSaveAndComeBack(Guid accreditationExternalId)
        {
            return await _httpSaveAndComeBackService.GetSaveAndComeBack(accreditationExternalId);
        }

        public async Task AddSaveAndComeBack(
            Guid accreditationExternalId,
            RouteValueDictionary routeDataValues)
        {
            

            // Get area
            var area = routeDataValues["area"]?.ToString();

            // Get controller
            var controller = routeDataValues["controller"]?.ToString();

            // Get action
            var action = routeDataValues["action"]?.ToString();
            var routeValues = new Dictionary<string, object>();

            foreach (var key in routeDataValues.Keys.Where(k => k != "area" && k != "controller" && k != "action"))
            {
                routeValues.Add(key, routeDataValues[key]);
            }

            await _httpSaveAndComeBackService.AddSaveAndComeBack(
                accreditationExternalId,
                new SaveAndComeBack
                {
                    Action = action,
                    Area = area,
                    Controller = controller,
                    Parameters = JsonConvert.SerializeObject(routeValues)
                });
        }

        public async Task DeleteSaveAndComeBack(Guid accreditationExternalId)
        {

        }
    }
}
