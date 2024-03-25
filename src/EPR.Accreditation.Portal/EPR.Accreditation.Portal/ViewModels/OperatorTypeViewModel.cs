using EPR.Accreditation.Facade.Common.Enums;
using System.ComponentModel.DataAnnotations;
using EPR.Accreditation.Portal.Resources;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class OperatorTypeViewModel
    {
        public Guid ExternalId { get; set; }

        [Required(ErrorMessageResourceName = "MissingSelectionErrorMessage", ErrorMessageResourceType = typeof(OperatorTypeResources))]
        public OperatorType OperatorType { get; set; }

    }
}
