using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.Extensions;
using EPR.Accreditation.Portal.Helpers.ActionFilters;
using EPR.Accreditation.Portal.Resources;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    [Route("Accreditation/{id}/Site/Material/{materialId}")]
    [ServiceFilter(typeof(WasteTypeActionFilter))]
    public class SiteMaterialController : BaseSiteController
    {
        private string SiteMaterialOutputsRouteName = "SiteMaterialOutputs";
        private string WasteLastYearRouteName = "WasteLastYear";

        public SiteMaterialController(
            IHttpContextAccessor httpContextAccessor,
            IUrlHelper urlHelper,
            IAccreditationSiteMaterialService accreditationSiteMaterialService,
            ISaveAndComeBackService saveAndComeBackService,
            BackPageViewModel backPageViewModel)
            : base(
                  httpContextAccessor,
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

        /// 
        /// STUBBED METHOD
        /// 
        [HttpGet("Material", Name = "SiteChooseMaterial")]
        public IActionResult ChooseMaterial(
            Guid? id,
            Guid? materialId)
        {
            return NotFound();
        }

        [HttpGet("WasteSource", Name = "SiteWasteSource")]
        public async Task<IActionResult> MaterialWasteSource(
            Guid? id,
            Guid? materialId)
        {
            if (id != null && 
                materialId != null)
                return await GetMaterialWasteSource(
                    id.Value,
                    null,
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
            Guid? materialId)
        {
            return NotFound();
        }

        [HttpGet("MaterialOutputs", Name = "SiteMaterialOutputs")]
        public async Task<IActionResult> MaterialOutputs(
            Guid? id,
            Guid? materialId)
        {
            if (id != null && 
                materialId != null)
            {
                PopulateBackModel(WasteLastYearRouteName);

                var materialOutputsViewModel = await _accreditationSiteMaterialService.GetMaterialOutputs(
                    id.Value,
                    materialId.Value);

                // if waste last year has not been set, then the user should not
                // be on this page
                if (materialOutputsViewModel.WasteLastYear == null)
                    return NotFound();

                // if waste last year is true then return the MaterialOutputs view
                if (materialOutputsViewModel.WasteLastYear == true)
                    return View("MaterialOutputsLastYear", materialOutputsViewModel);
                // otherwise return the annual outputs
                else
                    return View("MaterialOutputsEstimated", materialOutputsViewModel);
            }
            else
                return NotFound();
        }

        [HttpPost("MaterialOutputs")]
        public async Task<IActionResult> MaterialOutputs(
            MaterialOutputsViewModel viewModel,
            SaveButton saveButton)
        {
            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                MaterialOutputsLastYearResources.MaterialsNotProcessedBlank,
                MaterialOutputsLastYearResources.ContaminentsBlank,
                MaterialOutputsLastYearResources.ProcessLossBlank))
            {
                return await MaterialOutputs(
                    viewModel.Id,
                    viewModel.MaterialId);
            }

            await _accreditationSiteMaterialService.UpdateMaterialOutputs(viewModel);

            if (saveButton == SaveButton.SaveAndComeBack)
            {
                PopulateBackModel(SiteNonWasteInputsRouteName);

                // this is all the data we require to save for come back later
                await _saveAndComeBackService.AddSaveAndComeBack(
                    viewModel.Id,
                    Request.HttpContext.GetRouteData().Values);
                return View("_ApplicationSaved");
            }
            else
            {
                return RedirectToRoute(SiteProductsProducedRouteName,
                    new
                    {
                        viewModel.Id,
                        viewModel.MaterialId
                    });
            }
        }

        /// 
        /// STUBBED METHOD
        /// 
        [HttpGet("ProductsProduced", Name = "SiteProductsProduced")]
        public IActionResult ProductsProduced()
        {
            return NotFound();
        }

        /// 
        /// STUBBED METHOD
        /// 
        [HttpGet("EnterProcessingCapacity", Name = "EnterProcessingCapacity")]
        public IActionResult EnterProcessingCapacity()
        {
            return NotFound();
        }

        [HttpGet("WasteLastYear", Name = "WasteLastYear")]
        public async Task<IActionResult> WasteLastYear(
            Guid? id,
            Guid? materialId)
        {
            PopulateBackModel("EnterProcessingCapacity");

            if (id != null && 
                materialId != null)
            {
                var viewModel = await _accreditationSiteMaterialService.GetReprocessedWasteLastYearViewModel(
                    id.Value,
                    materialId.Value);

                return View(viewModel);
            }
            else
                return NotFound();
        }

        [HttpPost("WasteLastYear")]
        public async Task<IActionResult> WasteLastYear(
            ReprocessedWasteLastYearViewModel viewModel,
            SaveButton saveButton)
        {
            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                PermitExemptionResources.ErrorMessage))
                return View(viewModel);

            await _accreditationSiteMaterialService.UpdateReprocessedWasteLastYear(viewModel);

            if (saveButton == SaveButton.SaveAndContinue)
                return RedirectToRoute(
                    SiteMaterialOutputsRouteName, 
                    new
                    {
                        id = viewModel.Id,
                        viewModel.MaterialId
                    });

            // this is all the data we require to save for come back later
            await _saveAndComeBackService.AddSaveAndComeBack(
                viewModel.Id,
                Request.HttpContext.GetRouteData().Values);
            return View("_ApplicationSaved");
        }
    }
}