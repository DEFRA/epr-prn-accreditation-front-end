using EPR.Accreditation.Facade.Common.Enums;
using System.ComponentModel.DataAnnotations;
using EPR.Accreditation.Portal.Resources;
using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class HasOverseasAgentViewModel
    {

        public Guid ExternalId { get; set; }

        [Required(ErrorMessageResourceName = "MissingSelectionErrorMessage", ErrorMessageResourceType = typeof(HasOverseasAgentResources))]
        public bool? UseOverseasAgent { get; set; }

    }
}
