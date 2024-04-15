namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;

    public class WasteSourceViewModel
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public Guid MaterialId { get; set; }

        [Required(ErrorMessageResourceType = typeof(WasteSourceResources), ErrorMessageResourceName = "NoSourceSupplied")]
        [StringLength(200)]
        public string WasteSource { get; set; }
    }
}
