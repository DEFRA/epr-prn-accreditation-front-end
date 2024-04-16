namespace EPR.Accreditation.Portal.Services.Accreditation
{
    using EPR.Accreditation.Portal.DTOs.SaveAndComeBack;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using Newtonsoft.Json;

    public class SaveAndComeBackService : ISaveAndComeBackService
    {
        private readonly IHttpSaveAndComeBackService _httpSaveAndComeBackService;

        public SaveAndComeBackService(IHttpSaveAndComeBackService httpSaveAndComeBackService)
        {
            _httpSaveAndComeBackService = httpSaveAndComeBackService ?? throw new ArgumentNullException(nameof(httpSaveAndComeBackService));
        }

        public async Task AddSaveAndComeBack(
            Guid accreditationExternalId,
            RouteValueDictionary keyValuePairs)
        {
            // Get area
            var area = keyValuePairs["area"]?.ToString();

            // Get controller
            var controller = keyValuePairs["controller"]?.ToString();

            // Get action
            var action = keyValuePairs["action"]?.ToString();
            var routeValues = new Dictionary<string, object>();

            foreach (var key in keyValuePairs.Keys.Where(k => k != "area" && k != "controller" && k != "action"))
            {
                routeValues.Add(key, keyValuePairs[key]);
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
    }
}
