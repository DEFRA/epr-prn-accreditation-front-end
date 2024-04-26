namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Common.Enums;
    using EPR.Accreditation.Portal.Resources;

    public class OperatorTypeViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceName = "MissingSelectionErrorMessage", ErrorMessageResourceType = typeof(OperatorTypeResources))]
        public OperatorType? OperatorType { get; set; }
    }
}