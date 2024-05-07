namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;

    public class MaterialWasteInputsViewModel
    {
        /// <summary>
        /// Gets or sets accreditation id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets material id.
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// Gets or sets waste last year.
        /// </summary>
        public bool? WasteLastYear { get; set; }

        /// <summary>
        /// Gets or sets UkPackagingWaste.
        /// </summary>
        [Display(Name = "UkPackagingWasteField", ResourceType = typeof(MaterialWasteInputsLastYearResources))]
        [Required(ErrorMessageResourceType = typeof(MaterialWasteInputsLastYearResources), ErrorMessageResourceName = "UkPackagingWasteBlank")]
        [Range(0, 1000000D, ErrorMessageResourceType = typeof(MasterResources), ErrorMessageResourceName = "DecimalNumberFormatError")]
        public decimal? UkPackagingWaste { get; set; }

        /// <summary>
        /// Gets or sets NonUkPackagingWaste.
        /// </summary>
        [Display(Name = "NonUkPackagingWasteField", ResourceType = typeof(MaterialWasteInputsLastYearResources))]
        [Required(ErrorMessageResourceType = typeof(MaterialWasteInputsLastYearResources), ErrorMessageResourceName = "NonUkPackagingWasteBlank")]
        [Range(0, 1000000D, ErrorMessageResourceType = typeof(MasterResources), ErrorMessageResourceName = "DecimalNumberFormatError")]
        public decimal? NonUkPackagingWaste { get; set; }

        /// <summary>
        /// Gets or sets NonPackagingWaste.
        /// </summary>
        [Display(Name = "NonPackagingWasteField", ResourceType = typeof(MaterialWasteInputsLastYearResources))]
        [Required(ErrorMessageResourceType = typeof(MaterialWasteInputsLastYearResources), ErrorMessageResourceName = "NonPackagingWasteBlank")]
        [Range(0, 1000000D, ErrorMessageResourceType = typeof(MasterResources), ErrorMessageResourceName = "DecimalNumberFormatError")]
        public decimal? NonPackagingWaste { get; set; }
    }
}
