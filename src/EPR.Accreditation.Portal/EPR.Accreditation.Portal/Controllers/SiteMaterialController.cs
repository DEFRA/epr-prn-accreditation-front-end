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
        }

        [HttpGet("Accreditation/{id}/Site/{siteId}/Material/{materialId}/Material", Name = "SiteChooseMaterial")]
        public IActionResult ChooseMaterial(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            return NotFound();
        }

        [HttpGet("Accreditation/{id}/Site/{siteId}/Material/{materialId}/WasteSource", Name = "SiteWasteSource")]
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

        [HttpPost("Accreditation/{id}/Site/{siteId}/Material/{materialId}/WasteSource")]
        public async Task<IActionResult> MaterialWasteSource(
            WasteSourceViewModel viewModel,
            SaveButton saveButton)
        {
            return await SaveMaterialWasteSource(
                viewModel, 
                saveButton);
        }

        [HttpGet("Accreditation/{id}/Site/{siteId}/Material/{materialId}/ProcessingCapacity", Name = "SiteProcessingCapacity")]
        public IActionResult EnterProcessingCapacity(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            return NotFound();
        }

    }
}