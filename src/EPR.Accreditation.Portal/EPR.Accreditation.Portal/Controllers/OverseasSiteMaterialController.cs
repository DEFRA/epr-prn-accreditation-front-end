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

        [HttpGet("Accreditation/{id}/OverseasSite/{siteId}/Material/{materialId}/Material", Name = "OverseasSiteChooseMaterial")]
        public IActionResult ChooseMaterial(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            return NotFound();
        }

        [HttpGet("Accreditation/{id}/OverseasSite/{siteId}/Material/{materialId}/WasteSource", Name = "OverseasWasteSource")]
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

        [HttpPost("Accreditation/{id}/OverseasSite/{siteId}/Material/{materialId}/WasteSource")]
        public async Task<IActionResult> MaterialWasteSource(
            WasteSourceViewModel viewModel,
            SaveButton saveButton)
        {
            return await SaveMaterialWasteSource(
                viewModel,
                saveButton);
        }

        [HttpGet("Accreditation/{id}/OverseasSite/{siteId}/Material/{materialId}/ProcessingCapacity", Name = "OverseasSiteProcessingCapacity")]
        public IActionResult EnterProcessingCapacity(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            return NotFound();
        }

        [Route("Accreditation/{id}/{siteType}/{siteId}/Material/{materialId}/MaterialOutputs", Name = "OverseasSiteMaterialOutputs")]
        public async Task<IActionResult> MaterialOutputs(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            if (id != null && siteId != null && materialId != null)
                return await GetMaterialOutputs(
                    id.Value,
                    siteId.Value,
                    materialId.Value);
            else
                return NotFound();
        }
    }
}
