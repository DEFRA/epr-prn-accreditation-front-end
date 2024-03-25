using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.Helpers.ActionFilters;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    [ServiceFilter(typeof(WasteTypeActionFilter))]
    public class OverseasSiteMaterialController : BaseSiteController
    {
        public OverseasSiteMaterialController(IAccreditationSiteMaterialService accreditationSiteMaterialService) 
            : base(accreditationSiteMaterialService, SiteType.OverseasSite)
        {
        }

        [HttpGet("Accreditation/{id}/OverseasSite/{siteId}/Material/{materialId}")]
        public override async Task<IActionResult> MaterialWasteSource(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            return await base.MaterialWasteSource(
                id,
                siteId,
                materialId);
        }

        [HttpPost("Accreditation/{id}/OverseasSite/{siteId}/Material/{materialId}")]
        public override async Task<IActionResult> MaterialWasteSource(WasteSourceViewModel viewModel)
        {
            return await base.MaterialWasteSource(viewModel);
        }

    }
}
