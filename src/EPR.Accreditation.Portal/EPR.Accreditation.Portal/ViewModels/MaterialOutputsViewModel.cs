using EPR.Accreditation.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class MaterialOutputsViewModel
    {
        public Guid Id { get; set; }

        public Guid MaterialId { get; set; }

        public bool? WasteLastYear { get; set; }

        [Display(Name = "MaterialsNotProcessedField", ResourceType = typeof(MaterialOutputsResources))]
        [Required(ErrorMessageResourceType = typeof(MaterialOutputsResources), ErrorMessageResourceName = "MaterialsNotProcessedBlank")]
        [Range((double)0, (double)1000000, ErrorMessageResourceType = typeof(MasterResources), ErrorMessageResourceName = "DecimalNumberFormatError")]
        public decimal? TonnesNotProcessedOnSite { get; set; }

        [Display(Name = "ContaminentsField", ResourceType = typeof(MaterialOutputsResources))]
        [Required(ErrorMessageResourceType = typeof(MaterialOutputsResources), ErrorMessageResourceName = "ContaminentsBlank")]
        [Range((double)0, (double)1000000, ErrorMessageResourceType = typeof(MasterResources), ErrorMessageResourceName = "DecimalNumberFormatError")]
        public decimal? TonnesContaminents { get; set; }

        [Display(Name = "ProcessLossField", ResourceType = typeof(MaterialOutputsResources))]
        [Required(ErrorMessageResourceType = typeof(MaterialOutputsResources), ErrorMessageResourceName = "ProcessLossBlank")]
        [Range((double)0, (double)1000000, ErrorMessageResourceType = typeof(MasterResources), ErrorMessageResourceName = "DecimalNumberFormatError")]
        public decimal? TonnesProcessLoss { get; set; }
    }
}
