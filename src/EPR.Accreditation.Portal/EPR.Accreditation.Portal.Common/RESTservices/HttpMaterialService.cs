using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EPR.Accreditation.Facade.Common.RESTservices
{
    public class HttpMaterialService : BaseHttpService, IHttpMaterialService
    {
        public HttpMaterialService(
            IHttpContextAccessor httpContextAccessor, 
            IHttpClientFactory httpClientFactory, 
            string baseUrl, string endPointName) : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task<IEnumerable<Material>> GetAllMaterials()
        {
            return await Get<IEnumerable<Material>>(string.Empty, false);
        }
    }
}
