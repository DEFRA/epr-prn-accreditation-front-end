using EPR.Accreditation.API.Common.Dtos;
using EPR.Accreditation.Common.Services;
using EPRN.Portal.RESTServices.Interfaces;

namespace EPRN.Portal.RESTServices
{
    public class HttpAccreditationService : BaseHttpService, IHttpAccreditationService
    {
        public HttpAccreditationService(
                                        IHttpContextAccessor httpContextAccessor,
                                        IHttpClientFactory httpClientFactory,
                                        string baseUrl,
                                        string endPointName) : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task<int> CreateWastePermit(
            int id,
            int accreditationId)
        {
            return await Post<int>($"Create/Id/{id}/{accreditationId}");
        }
        public async Task<WastePermit> GetWastePermit(int id)
        {
            return await Get<WastePermit?>($"{id}");
        }

        public async Task<Accreditation> GetAccreditation(string id)
        {
            return await Get<Accreditation?>($"{id}");
        }

        public async Task<Accreditation> SaveAccreditation(string id)
        {
            return await Put<Accreditation>($"{id}");
        }
    }
}
