using EPR.Accreditation.Portal.DTOs;
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

        public async Task CreateWastePermit(Guid id, DTOs.WastePermit wastePermit)
        {
            await Post($"{id}/WastePermit", wastePermit);
        }
        
        public async Task<WastePermit> GetWastePermit(Guid id)
        {
           return await Get<WastePermit>($"{id}/WastePermit");
        }
    }
}
