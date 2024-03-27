using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.DTOs.WastePermit
{
    public class LicensesAndPermitsReferences
    {
        public int AccreditationId { get; set; }

        [MaxLength(100)]
        public string DealerRegistrationNumber { get; set; }

        [MaxLength(100)]
        public string EnvironmentalPermitNumber { get; set; }

        [MaxLength(10)]
        public string PartAActivityReferenceNumber { get; set; }

        [MaxLength(10)]
        public string PartBActivityReferenceNumber { get; set; }

        [MaxLength(50)]
        public string DischargeConsentNumber { get; set; }
    }
}
