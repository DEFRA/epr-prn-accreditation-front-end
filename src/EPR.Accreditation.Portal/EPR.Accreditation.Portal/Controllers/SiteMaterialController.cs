using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.Extensions;
using EPR.Accreditation.Portal.Helpers.ActionFilters;
using EPR.Accreditation.Portal.Resources;
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
            SiteProductsProducedRouteName = "SiteProductsProduced";
            SiteChooseMaterialRouteName = "SiteChooseMaterial";
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

        [HttpGet("Accreditation/{id}/Site/{siteId}/Material/{materialId}/MaterialOutputs", Name = "SiteMaterialOutputs")]
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

        [HttpPost("Accreditation/{id}/Site/{siteId}/Material/{materialId}/MaterialOutputs")]
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
        [HttpGet("Accreditation/{id}/Site/{siteId}/Material/{materialId}/ProductsProduced", Name = "SiteProductsProduced")]
        public IActionResult ProductsProduced()
        {
            return NotFound();
        }

        [HttpGet("Accreditation/{id}/Site/{siteId}/Material/{materialId}/WasteLastYear")]
        public async Task<IActionResult> WasteLastYear(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            // TODO: Need to add correct back link in the future
            _backPageViewModel.Url = _urlHelper.ActionLink("ApplyForAccreditation", "Home");

            if (id == null)
                return NotFound();

            if (siteId == null)
                return NotFound();

            if (materialId == null)
                return NotFound();

            var viewModel = await _accreditationSiteMaterialService.GetReprocessedWasteLastYearViewModel(
                id.Value,
                siteId.Value,
                materialId.Value
                );

            return View(viewModel);
        }

        [HttpPost("Accreditation/{id}/Site/{siteId}/Material/{materialId}/WasteLastYear")]
        public async Task<IActionResult> WasteLastYear(
            ReprocessedWasteLastYearViewModel viewModel,
            SaveButton saveButton)
        {
            if (!ModelState.IsValidForSaveForLater(
            saveButton,
                PermitExemptionResources.ErrorMessage))
                return View(viewModel);

            await _accreditationSiteMaterialService.UpdateReprocessedWasteLastYear(viewModel);

            if (saveButton == SaveButton.SaveAndContinue && viewModel.HasReprocessedWasteLastYear.Value == true)
                return RedirectToAction("EnterWasteInputs", "Accreditation");

            else if (saveButton == SaveButton.SaveAndContinue && viewModel.HasReprocessedWasteLastYear.Value == false)
                return RedirectToAction("EstimateAnnualWasteInputs", "Accreditation");

            // this is all the data we require to save for come back later
            await _saveAndComeBackService.AddSaveAndComeBack(
                viewModel.Id,
                Request.HttpContext.GetRouteData().Values);
            return View("_ApplicationSaved");
        }
    }
}