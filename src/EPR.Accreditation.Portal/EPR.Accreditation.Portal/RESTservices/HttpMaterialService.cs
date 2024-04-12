namespace EPR.Accreditation.Portal.RESTservices
{
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;
    using Microsoft.AspNetCore.Http;

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
