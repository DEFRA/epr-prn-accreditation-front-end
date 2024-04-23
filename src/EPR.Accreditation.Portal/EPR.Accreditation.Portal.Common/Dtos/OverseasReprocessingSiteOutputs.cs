namespace EPR.Accreditation.Facade.Common.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class OverseasReprocessingSiteOutputs
    {
        public Guid? ExternalId { get; set; }

        [MaxLength(500)]
        public string Outputs { get; set; }
    }
}
