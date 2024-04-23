namespace EPR.Accreditation.Portal.DTOs.Country
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// DTO for the country
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Gets or sets the country ID
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the country
        /// </summary>
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
