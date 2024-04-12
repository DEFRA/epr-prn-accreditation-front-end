namespace EPR.Accreditation.Portal.Controllers
{
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Extensions;
    using EPR.Accreditation.Portal.Helpers.ActionFilters;
    using EPR.Accreditation.Portal.Helpers.Interfaces;
    using EPR.Accreditation.Portal.Options;
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Controller for Site (Not overseas sites) materials
    /// </summary>
    [Route("Accreditation/{id}/Site/Material/{materialId}")]
    [ServiceFilter(typeof(WasteTypeActionFilter))]
    public class SiteMaterialController : BaseSiteController
    {
        private string SiteMaterialOutputsRouteName = "SiteMaterialOutputs";
        private string WasteLastYearRouteName = "WasteLastYear";
        private string NonWasteInputsRouteName = "NonWasteInputs";
        private int _maximumMultiLineRecordNumber = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteMaterialController"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The http context accessor</param>
        /// <param name="urlHelper">Wrapper to the IUrlHelper - makes testing easier</param>
        /// <param name="accreditationSiteMaterialService">The service for performing any busines logic for accreditation site materials</param>
        /// <param name="saveAndComeBackService">The service for save and continue</param>
        /// <param name="appSettingsConfiguration">The app settings configuration</param>
        /// <param name="backPageViewModel">The view model for populating the back button/link</param>
        public SiteMaterialController(
            IHttpContextAccessor httpContextAccessor,
            IUrlHelperWrapper urlHelper,
            IAccreditationSiteMaterialService accreditationSiteMaterialService,
            ISaveAndComeBackService saveAndComeBackService,
            IOptions<AppSettingsConfigOptions> appSettingsConfiguration,
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

            if (appSettingsConfiguration?.Value?.MaximumMultiLineRecordNumber == null)
            {
                throw new ArgumentNullException(nameof(appSettingsConfiguration));
            }

            _maximumMultiLineRecordNumber = appSettingsConfiguration.Value.MaximumMultiLineRecordNumber.Value;
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

        /// <summary>
        /// Get method for returning the Non waste inputs view for an exporter. Can be one of
        /// 2 views depending upon the value of whether waste last year was processed.
        /// </summary>
        /// <param name="id">Accreditation Id</param>
        /// <param name="materialId">The material ID</param>
        /// <returns>Various different return responses</returns>
        [HttpGet("NonWasteInputs", Name = "NonWasteInputs")]
        public async Task<IActionResult> NonWasteInputs(
            Guid? id,
            Guid? materialId)
        {
            if (id.HasValue &&
                materialId.HasValue)
            {
                PopulateBackModel(WasteLastYearRouteName);

                var nonWasteInputsViewModel = await _accreditationSiteMaterialService.GetNonWasteInputs(
                    id.Value,
                    materialId.Value);

                // if waste last year has not been set, then the user should not
                // be on this page
                if (nonWasteInputsViewModel.WasteLastYear == null)
                    return NotFound();

                // if waste last year is true then return the MaterialOutputs view
                if (nonWasteInputsViewModel.WasteLastYear == true)
                    return View("NonWasteInputsLastYear", nonWasteInputsViewModel);
                else // otherwise return the annual outputs
                    return View("NonWasteInputsEstimated", nonWasteInputsViewModel);
            }

            return NotFound();
        }

        /// <summary>
        /// Post method for updating the Non waste inputs for an exporter.
        /// </summary>
        /// <param name="viewModel">The view model for non waste inputs</param>
        /// <param name="saveButton">The value of the button used to post the data</param>
        /// <returns>Either the original view with a new row (if javascript is turned off),
        /// original view with validation errors, a view signfying that progress has been saved
        /// or a redirect to the next step</returns>
        [HttpPost("NonWasteInputs")]
        public async Task<IActionResult> NonWasteInputs(
            NonWasteInputsViewModel viewModel,
            SaveButton saveButton)
        {
            PopulateBackModel(WasteLastYearRouteName);

            if (saveButton == SaveButton.AddRow)
            {
                // We're adding a new row, therefore we do not want to perform any validation
                ModelState.Clear();

                // user is adding a new row (without javascript) and therefore we need to
                // return the view with a new row added
                if (viewModel.RowsToDisplay < _maximumMultiLineRecordNumber)
                {
                    viewModel.RowsToAdd++;
                }

                return View(
                    viewModel.WasteLastYear.Value ? "NonWasteInputsLastYear" : "NonWasteInputsEstimated",
                    viewModel);
            }

            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                MaterialOutputsLastYearResources.MaterialsNotProcessedBlank,
                MaterialOutputsLastYearResources.ContaminentsBlank,
                MaterialOutputsLastYearResources.ProcessLossBlank))
            {
                if (viewModel.WasteLastYear == true)
                    return View("NonWasteInputsLastYear", viewModel);
                else
                    return View("NonWasteInputsEstimated", viewModel);
            }

            await _accreditationSiteMaterialService.UpdateNonWasteInputs(viewModel);

            if (saveButton == SaveButton.SaveAndComeBack)
            {
                PopulateBackModel(SiteNonWasteInputsRouteName);

                // this is all the data we require to save for come back later
                await _saveAndComeBackService.AddSaveAndComeBack(
                    viewModel.Id,
                    _httpContextAccessor.HttpContext.GetRouteData().Values);
                return View("_ApplicationSaved");
            }
            else
            {
                return RedirectToRoute(SiteMaterialOutputsRouteName,
                    new
                    {
                        viewModel.Id,
                        viewModel.MaterialId
                    });
            }
        }

        [HttpGet("MaterialOutputs", Name = "SiteMaterialOutputs")]
        public async Task<IActionResult> MaterialOutputs(
            Guid? id,
            Guid? materialId)
        {
            if (id != null &&
                materialId != null)
            {
                PopulateBackModel(NonWasteInputsRouteName);

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
                    _httpContextAccessor.HttpContext.GetRouteData().Values);
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

        /// <summary>
        /// Returns a page containing annual waste (actual or estimated) data.
        /// </summary>
        /// <param name="id">Accreditation id.</param>
        /// <param name="materialId">Material id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet("MaterialWasteOutputs", Name = "SiteMaterialWasteOutputs")]
        public async Task<IActionResult> MaterialWasteOutputs(
            Guid? id,
            Guid? materialId)
        {
            if (id == null || materialId == null)
            {
                return this.NotFound();
            }

            this.PopulateBackModel(this.WasteLastYearRouteName);

            MaterialWasteOutputsViewModel materialWasteOutputsViewModel = await this._accreditationSiteMaterialService.GetMaterialWasteOutputs(
                id.Value,
                materialId.Value);

            // if waste last year has not been set, then the user should not
            // be on this page
            if (materialWasteOutputsViewModel.WasteLastYear == null)
            {
                return this.NotFound();
            }

            // if waste last year is true then return the MaterialWasteOutputs view
            // otherwise, return the estimated view
            return materialWasteOutputsViewModel.WasteLastYear == true ?
                this.View("MaterialWasteOutputsLastYear", materialWasteOutputsViewModel) :
                this.View("MaterialWasteOutputsEstimated", materialWasteOutputsViewModel);
        }

        /// <summary>
        /// Saves annual waste data.
        /// </summary>
        /// <param name="viewModel">View model used for data input.</param>
        /// <param name="saveButton">Save button.</param>
        /// <returns>Does a page redirect.</returns>
        [HttpPost("MaterialWasteOutputs")]
        public async Task<IActionResult> MaterialWasteOutputs(
            MaterialWasteOutputsViewModel viewModel,
            SaveButton saveButton)
        {
            if (!this.ModelState.IsValidForSaveForLater(
                saveButton,
                MaterialWasteOutputsLastYearResources.UkPackagingWasteBlank,
                MaterialWasteOutputsLastYearResources.NonUkPackagingWasteBlank,
                MaterialWasteOutputsLastYearResources.NonPackagingWasteBlank))
            {
                return await this.MaterialWasteOutputs(
                    viewModel.Id,
                    viewModel.MaterialId);
            }

            await this._accreditationSiteMaterialService.UpdateMaterialWasteOutputs(viewModel);

            if (saveButton == SaveButton.SaveAndComeBack)
            {
                this.PopulateBackModel(this.SiteNonWasteInputsRouteName);

                // this is all the data we require to save for come back later
                await this._saveAndComeBackService.AddSaveAndComeBack(
                    viewModel.Id,
                    this._httpContextAccessor.HttpContext.GetRouteData().Values);
                return this.View("_ApplicationSaved");
            }
            else
            {
                return this.RedirectToRoute(
                    this.SiteProductsProducedRouteName,
                    new
                    {
                        viewModel.Id,
                        viewModel.MaterialId,
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
                    NonWasteInputsRouteName,
                    new
                    {
                        id = viewModel.Id,
                        viewModel.MaterialId
                    });

            // this is all the data we require to save for come back later
            await _saveAndComeBackService.AddSaveAndComeBack(
                viewModel.Id,
                _httpContextAccessor.HttpContext.GetRouteData().Values);
            return View("_ApplicationSaved");
        }
    }
}