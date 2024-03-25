using EPR.Accreditation.Portal.RESTservices.Interfaces;

namespace EPR.Accreditation.Portal.RESTservices
{
    public class HttpAccreditionService : BaseHttpService, IHttpAccreditationService
    {
        public HttpAccreditionService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName) : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task CreateAccreditation(Guid id, DTOs.Accreditation accreditation)
        {
            await Post($"{id}", accreditation);
        }
    }
}
