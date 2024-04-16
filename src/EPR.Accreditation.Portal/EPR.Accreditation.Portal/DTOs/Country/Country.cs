namespace EPR.Accreditation.Portal.DTOs.Country
{
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        public int CountryId { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }
    }
}
