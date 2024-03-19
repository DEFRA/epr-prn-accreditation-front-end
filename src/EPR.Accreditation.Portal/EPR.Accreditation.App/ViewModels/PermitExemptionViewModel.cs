using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.App.ViewModels
{
    public class PermitExemptionViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public bool HasPermitExemption { get; set; }
    }
}
