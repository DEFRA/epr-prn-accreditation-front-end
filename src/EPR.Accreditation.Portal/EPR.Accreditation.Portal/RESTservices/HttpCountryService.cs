namespace EPR.Accreditation.Portal.RESTservices
{
    using EPR.Accreditation.Portal.Common.RESTservices;
    using EPR.Accreditation.Portal.DTOs.Country;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;

    /// <summary>
    /// Service for the Http Country Service
    /// </summary>
    public class HttpCountryService : BaseHttpService, IHttpCountryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpCountryService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">Injecting the context accessor</param>
        /// <param name="httpClientFactory">Injecting the client factory</param>
        /// <param name="baseUrl">Declaring the base URL</param>
        /// <param name="endPointName">Declaring the name of the endpoint</param>
        public HttpCountryService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName)
            : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        /// <summary>
        /// Returns the list of countries in the world
        /// </summary>
        /// <returns>A list of countries in the world</returns>
        public async Task<IEnumerable<Country>> GetCountryList()
        {
            return await Get<IEnumerable<Country>>(string.Empty, false);
        }
    }
}
