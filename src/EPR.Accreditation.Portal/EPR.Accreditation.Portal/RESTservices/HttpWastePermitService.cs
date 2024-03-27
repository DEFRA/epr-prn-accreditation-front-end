using EPR.Accreditation.Portal.DTOs.WastePermit;
using EPR.Accreditation.Portal.RESTservices.Interfaces;

namespace EPR.Accreditation.Portal.RESTservices
{
    public class HttpWastePermitService : BaseHttpService, IHttpWastePermitService
    {
        public HttpWastePermitService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName) : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task<bool?> GetHasPermitExemption(Guid id)
        {
            return await Get<bool?>($"{id}/WastePermitExemption");
        }

        public async Task UpdatePermitExemption(Guid id, PermitExemption permitExemption)
        {
            await Put($"{id}/WastePermitExemption", permitExemption);
        }
    }
}
