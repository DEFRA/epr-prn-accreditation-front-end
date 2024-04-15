<<<<<<<< HEAD:src/EPR.Accreditation.Portal/EPR.Accreditation.Portal/ViewModels/SiteMaterial/WasteSource.cs
﻿using EPR.Accreditation.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.ViewModels.SiteMaterial
========
﻿namespace EPR.Accreditation.Portal.ViewModels
>>>>>>>> dev:src/EPR.Accreditation.Portal/EPR.Accreditation.Portal/ViewModels/WasteSourceViewModel.cs
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
