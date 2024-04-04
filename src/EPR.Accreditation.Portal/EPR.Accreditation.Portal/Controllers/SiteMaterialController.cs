using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.Helpers.ActionFilters;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    [Route("Accreditation/{id}/Site/{siteId}/Material/{materialId}")]
    [ServiceFilter(typeof(WasteTypeActionFilter))]
    public class SiteMaterialController : BaseSiteController
    {
        public SiteMaterialController(
            IUrlHelper urlHelper,
            IAccreditationSiteMaterialService accreditationSiteMaterialService,
            ISaveAndComeBackService saveAndComeBackService,
            BackPageViewModel backPageViewModel)
            : base(
                  urlHelper,
                  accreditationSiteMaterialService,
                  saveAndComeBackService,
                  backPageViewModel,
                  SiteType.Site)
        {
            SiteProcessingCapacityRouteName = "SiteProcessingCapacity";
            SiteProductsProducedRouteName = "SiteProductsProduced";
            SiteChooseMaterialRouteName = "SiteChooseMaterial";
        }

        [HttpGet("Material", Name = "SiteChooseMaterial")]
        public IActionResult ChooseMaterial(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            return NotFound();
        }

        [HttpGet("WasteSource", Name = "SiteWasteSource")]
        public async Task<IActionResult> MaterialWasteSource(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            if (id != null && siteId != null && materialId != null)
                return await GetMaterialWasteSource(
                    id.Value,
                    siteId.Value,
                    materialId.Value);
            else
                return NotFound();
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

        [HttpGet("ProcessingCapacity", Name = "SiteProcessingCapacity")]
        public IActionResult EnterProcessingCapacity(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            return NotFound();
        }

        [HttpGet("MaterialOutputs", Name = "SiteMaterialOutputs")]
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

        [HttpPost("MaterialOutputs")]
        public async Task<IActionResult> MaterialOutputs(
            MaterialOutputsViewModel materialOutputsViewModel,
            SaveButton saveButton)
        {
            return await SaveMaterialOutputs(
                materialOutputsViewModel,
                saveButton);
        }

        /// 
        /// STUBBED METHOD
        /// 
        [HttpGet("ProductsProduced", Name = "SiteProductsProduced")]
        public IActionResult ProductsProduced()
        {
            return NotFound();
        }
    }
}