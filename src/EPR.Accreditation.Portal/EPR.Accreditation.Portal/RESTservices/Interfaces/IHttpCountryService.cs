namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    using EPR.Accreditation.Portal.DTOs.Country;

    /// <summary>
    /// Interface for the Http country service
    /// </summary>
    public interface IHttpCountryService
    {
        /// <summary>
        /// Gets a list of countries in the world
        /// </summary>
        /// <returns>A list of countries in the world</returns>
        Task<IEnumerable<Country>> GetCountryList();
    }
}
