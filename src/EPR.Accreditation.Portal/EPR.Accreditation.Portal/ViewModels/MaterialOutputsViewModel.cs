using EPR.Accreditation.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class MaterialOutputsViewModel
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public Guid MaterialId { get; set; }

        [Display(Name = "MaterialsNotProcessedField", ResourceType = typeof(MaterialOutputsResources))]
        [Required(ErrorMessageResourceType = typeof(MaterialOutputsResources), ErrorMessageResourceName = "FieldEntryMissing")]
        [Range((double)0, (double)1000000, ErrorMessageResourceType = typeof(MasterResources), ErrorMessageResourceName = "DecimalNumberFormatError")]
        public decimal? TonnesNotProcessedOnSite { get; set; }

        [Display(Name = "ContaminentsField", ResourceType = typeof(MaterialOutputsResources))]
        [Required(ErrorMessageResourceType = typeof(MaterialOutputsResources), ErrorMessageResourceName = "FieldEntryMissing")]
        [Range((double)0, (double)1000000, ErrorMessageResourceType = typeof(MasterResources), ErrorMessageResourceName = "DecimalNumberFormatError")]
        public decimal? TonnesContaminents { get; set; }

        [Display(Name = "ProcessLossField", ResourceType = typeof(MaterialOutputsResources))]
        [Required(ErrorMessageResourceType = typeof(MaterialOutputsResources), ErrorMessageResourceName = "FieldEntryMissing")]
        [Range((double)0, (double)1000000, ErrorMessageResourceType = typeof(MasterResources), ErrorMessageResourceName = "DecimalNumberFormatError")]
        public decimal? TonnesProcessLoss { get; set; }
    }
}
