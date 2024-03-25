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

        public Task DeleteSaveAndComeBack(Guid accreditationExternalId)
        {
            throw new NotImplementedException();
        }

        public Task<SaveAndComeBack> GetSaveAndComeBack(Guid accreditationExternalId)
        {
            throw new NotImplementedException();
        }
    }
}
