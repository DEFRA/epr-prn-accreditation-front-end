namespace EPR.Accreditation.Portal.RESTservices
{
    using EPR.Accreditation.Portal.Common.RESTservices;
    using EPR.Accreditation.Portal.DTOs.Country;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;

    public class HttpCountryService : BaseHttpService, IHttpCountryService
    {
        public HttpCountryService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName)
            : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task<IEnumerable<Country>> GetCountryList()
        {
            return await Get<IEnumerable<Country>>(string.Empty, false);
        }
    }
}
