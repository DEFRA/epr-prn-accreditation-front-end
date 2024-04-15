namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class OverseasReprocessingSiteOutputsViewModel
    {
        public Guid? ExternalId { get; set; }

        [MaxLength(500)]
        public string Outputs { get; set; }
    }
}
