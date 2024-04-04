using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.Helpers.ActionFilters;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    [Route("Accreditation/{id}/OverseasSite/{siteId}/Material/{materialId}")]
    [ServiceFilter(typeof(WasteTypeActionFilter))]
    public class OverseasSiteMaterialController : BaseSiteController
    {
        public OverseasSiteMaterialController(
            IUrlHelper urlHelper,
            IAccreditationSiteMaterialService accreditationSiteMaterialService,
            ISaveAndComeBackService saveAndComeBackService,
            BackPageViewModel backPageViewModel)
            : base(
                  urlHelper,
                  accreditationSiteMaterialService,
                  saveAndComeBackService,
                  backPageViewModel,
                  SiteType.OverseasSite)
        {
            SiteProcessingCapacityRouteName = "OverseasSiteProcessingCapacity";
            SiteProductsProducedRouteName = "OverseasSiteProductsProduced";
            SiteChooseMaterialRouteName = "OverseasSiteChooseMaterial";
        }

        [HttpGet("Material", Name = "OverseasSiteChooseMaterial")]
        public IActionResult ChooseMaterial(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            return NotFound();
        }

        [HttpGet("WasteSource", Name = "OverseasWasteSource")]
        public async Task<IActionResult> MaterialWasteSource(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            return await GetMaterialWasteSource(
                id,
                siteId,
                materialId);
        }

        [HttpPost("WasteSource")]
        public async Task<IActionResult> MaterialWasteSource(
            WasteSourceViewModel viewModel,
            SaveButton saveButton)
        {
            return await SaveMaterialWasteSource(
                viewModel,
                saveButton);
        }

        [HttpGet("ProcessingCapacity", Name = "OverseasSiteProcessingCapacity")]
        public IActionResult EnterProcessingCapacity(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            return NotFound();
        }
    }
}
