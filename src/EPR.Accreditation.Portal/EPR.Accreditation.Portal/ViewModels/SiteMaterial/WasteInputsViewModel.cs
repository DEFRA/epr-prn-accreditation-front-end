namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;

    public class WasteInputsViewModel
    {
        public Guid Id { get; set; }

        public Guid MaterialId { get; set; }

        public bool? WasteLastYear { get; set; }

        [Display(Name = "MaterialsNotProcessedField", ResourceType = typeof(MaterialOutputsLastYearResources))]
        [Required(ErrorMessageResourceType = typeof(MaterialOutputsLastYearResources), ErrorMessageResourceName = "MaterialsNotProcessedBlank")]
        [Range(0, 1000000, ErrorMessageResourceType = typeof(MasterResources), ErrorMessageResourceName = "DecimalNumberFormatError")]
        public decimal? TonnesNotProcessedOnSite { get; set; }

        [Display(Name = "ContaminentsField", ResourceType = typeof(MaterialOutputsLastYearResources))]
        [Required(ErrorMessageResourceType = typeof(MaterialOutputsLastYearResources), ErrorMessageResourceName = "ContaminentsBlank")]
        [Range(0, 1000000, ErrorMessageResourceType = typeof(MasterResources), ErrorMessageResourceName = "DecimalNumberFormatError")]
        public decimal? TonnesContaminents { get; set; }

        [Display(Name = "ProcessLossField", ResourceType = typeof(MaterialOutputsLastYearResources))]
        [Required(ErrorMessageResourceType = typeof(MaterialOutputsLastYearResources), ErrorMessageResourceName = "ProcessLossBlank")]
        [Range(0, 1000000, ErrorMessageResourceType = typeof(MasterResources), ErrorMessageResourceName = "DecimalNumberFormatError")]
        public decimal? TonnesProcessLoss { get; set; }
    }
}
