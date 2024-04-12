namespace EPR.Accreditation.Portal.RESTservices
{
    using EPR.Accreditation.Portal.DTOs.SaveAndComeBack;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;

    public class HttpSaveAndComeBackService : BaseHttpService, IHttpSaveAndComeBackService
    {
        public HttpSaveAndComeBackService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName) : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task AddSaveAndComeBack(
            Guid accreditationExternalId,
            SaveAndComeBack saveAndBack)
        {
            await Post($"{accreditationExternalId}", saveAndBack);
        }

        public async Task DeleteSaveAndComeBack(Guid accreditationExternalId)
        {
            await Delete($"{accreditationExternalId}");
        }

        public async Task<SaveAndComeBack> GetSaveAndComeBack(Guid accreditationExternalId)
        {
            return await Get<SaveAndComeBack>($"{accreditationExternalId}");
        }
    }
}
