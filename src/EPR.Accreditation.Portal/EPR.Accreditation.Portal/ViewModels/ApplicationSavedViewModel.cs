﻿using EPR.Accreditation.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class ApplicationSavedViewModel
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public Guid MaterialId { get; set; }

        [Required(ErrorMessageResourceType = typeof(WasteSourceResources), ErrorMessageResourceName = "NoSourceSupplied")]
        [StringLength(200)]
        public string WasteSource { get; set; }

        public string WasteDestination { get; set; }

        public int ApplicationExpiry { get; set; }
    }
}
