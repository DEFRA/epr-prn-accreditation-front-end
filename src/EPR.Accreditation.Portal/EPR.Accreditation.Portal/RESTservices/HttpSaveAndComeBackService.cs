using EPR.Accreditation.Portal.DTOs.SaveAndComeBack;
using EPR.Accreditation.Portal.RESTservices.Interfaces;

namespace EPR.Accreditation.Portal.RESTservices
{
    public class HttpSaveAndComeBackService : BaseHttpService, IHttpSaveAndComeBackService
    {
        public HttpSaveAndComeBackService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName) : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task AddSaveAndComeBack(Guid accreditationExternalId, SaveAndComeBack saveAndComeBack)
        {
            await Post($"{accreditationExternalId}", saveAndComeBack);
        }

        public async Task DeleteSaveAndComeBack(Guid accreditationExternalId)
        {
            await Delete($"{accreditationExternalId}");
        }

        public Task<SaveAndComeBack> GetSaveAndComeBack(Guid accreditationExternalId)
        {
            throw new NotImplementedException();
        }
    }
}
