using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class ExemptionReferencesViewModel
    {
        [Required(ErrorMessage = "Reference Number 2 is required")]
        public string ReferenceNumber1 { get; set; }

        [Required(ErrorMessage = "Reference Number 2 is required")]
        public string ReferenceNumber2 { get; set; }

        [Required(ErrorMessage = "Reference Number 3 is required")]
        public string ReferenceNumber3 { get; set; }

        [Required(ErrorMessage = "Reference Number 4 is required")]
        public string ReferenceNumber4 { get; set; }

        [Required(ErrorMessage = "Reference Number 5 is required")]
        public string ReferenceNumber5 { get; set; }
    }
}
