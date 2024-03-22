using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.Helpers.ActionFilters;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    [ServiceFilter(typeof(WasteTypeActionFilter))]
    public class SiteMaterialController : BaseSiteController
    {
        public SiteMaterialController(IAccreditationSiteMaterialService accreditationSiteMaterialService) 
            : base(accreditationSiteMaterialService, SiteType.Site)
        {
        }

        [HttpGet("Accreditation/{id}/Site/{siteId}/Material/{materialId}")]
        public override async Task<IActionResult> MaterialWasteSource(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            if (id != null && siteId != null && materialId != null)
                return await base.MaterialWasteSource(
                    id.Value,
                    siteId.Value,
                    materialId.Value);
            else
                return NotFound();
        }

        [HttpPost("Accreditation/{id}/Site/{siteId}/Material/{materialId}")]
        public override async Task<IActionResult> MaterialWasteSource(WasteSourceViewModel viewModel)
        {
            return await base.MaterialWasteSource(viewModel);
        }
    }
}
