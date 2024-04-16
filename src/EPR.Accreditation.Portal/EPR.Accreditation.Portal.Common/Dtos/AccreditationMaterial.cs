namespace EPR.Accreditation.Portal.Common.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class AccreditationMaterial
    {
        public int Id { get; set; }

        public Guid ExternalId { get; set; }

        public decimal AnnualCapacity { get; set; }

        public decimal WeeklyCapacity { get; set; }

        [MaxLength(200)]
        public string WasteSource { get; set; }
    }
}
