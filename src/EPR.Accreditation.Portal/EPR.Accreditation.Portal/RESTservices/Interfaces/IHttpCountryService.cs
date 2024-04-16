using EPR.Accreditation.Portal.DTOs.Country;

namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    public interface IHttpCountryService
    {
        Task<IEnumerable<Country>> GetCountryList();
    }
}
