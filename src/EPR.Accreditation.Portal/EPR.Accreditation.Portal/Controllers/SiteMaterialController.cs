namespace EPR.Accreditation.Portal.Controllers
{
    using EPR.Accreditation.Portal.Attributes.ActionFilters;
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Extensions;
    using EPR.Accreditation.Portal.Helpers.Interfaces;
    using EPR.Accreditation.Portal.Options;
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using EPR.Accreditation.Portal.ViewModels.SiteMaterial;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Controller for Site (Not overseas sites) materials
    /// </summary>
    [Route("Accreditation/{id}/Site/Material/{materialId}")]
    [ServiceFilter(typeof(WasteTypeActionFilter))]
    public class SiteMaterialController : BaseSiteController
    {
        // Routename string constants
        private const string SiteMaterialOutputsRouteName = "SiteMaterialOutputs";
        private const string WasteLastYearRouteName = "WasteLastYear";
        private const string NonWasteInputsRouteName = "NonWasteInputs";
        private const string ProductsProducedRouteName = "ProductsProduced";
        private const string AuthorityRouteName = "Authority";

        // Viewname string constants
        private const string MaterialOutputsLastYearView = "MaterialOutputsLastYear";
        private const string MaterialOutputsEstimatedView = "MaterialOutputsEstimated";
        private const string ProductsProducedLastYearView = "ProductsProducedLastYear";
        private const string ProductsProducedEstimatedView = "ProductsProducedEstimated";
        private const string NonWasteInputsLastYearView = "NonWasteInputsLastYear";
        private const string NonWasteInputsEstimatedView = "NonWasteInputsEstimated";

        private readonly int _initialTypeTonnesRows = 0;
        private readonly int _maximumMultiLineRecordNumber = 0;

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
            _siteProcessingCapacityRouteName = "SiteProcessingCapacity";
            _siteChooseMaterialRouteName = "SiteChooseMaterial";

            if (appSettingsConfiguration?.Value?.MaximumMultiLineRecordNumber == null)
            {
                throw new ArgumentNullException(nameof(appSettingsConfiguration.Value.MaximumMultiLineRecordNumber));
            }

            if (appSettingsConfiguration?.Value?.InitialTypeTonnesRows == null)
            {
                throw new ArgumentNullException(nameof(appSettingsConfiguration.Value.InitialTypeTonnesRows));
            }

            _initialTypeTonnesRows = appSettingsConfiguration.Value.InitialTypeTonnesRows.Value;
            _maximumMultiLineRecordNumber = appSettingsConfiguration.Value.MaximumMultiLineRecordNumber.Value;
        }

        /// <summary>
        /// Stubbed method
        /// </summary>
        /// <param name="id">the id of the accreditation</param>
        /// <returns>NotFound</returns>
        [HttpGet("Material", Name = "SiteChooseMaterial")]
        public IActionResult ChooseMaterial(
            Guid? id)
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
            {
                return await GetMaterialWasteSource(
                    id.Value,
                    null,
                    materialId.Value);
            }
            else
            {
                return NotFound();
            }
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
                {
                    return NotFound();
                }

                // if waste last year is true then return the MaterialOutputs view
                if (nonWasteInputsViewModel.WasteLastYear == true)
                {
                    return View(NonWasteInputsLastYearView, nonWasteInputsViewModel);
                }
                else
                {
                    // otherwise return the annual outputs
                    return View(NonWasteInputsEstimatedView, nonWasteInputsViewModel);
                }
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
                if (viewModel.RowsToDisplay(_initialTypeTonnesRows) < _maximumMultiLineRecordNumber)
                {
                    viewModel.RowsToAdd++;
                }

                return View(
                    viewModel.WasteLastYear.Value ? NonWasteInputsLastYearView : NonWasteInputsEstimatedView,
                    viewModel);
            }

            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                MaterialOutputsLastYearResources.MaterialsNotProcessedBlank,
                MaterialOutputsLastYearResources.ContaminentsBlank,
                MaterialOutputsLastYearResources.ProcessLossBlank))
            {
                if (viewModel.WasteLastYear == true)
                {
                    return View(NonWasteInputsLastYearView, viewModel);
                }
                else
                {
                    return View(NonWasteInputsEstimatedView, viewModel);
                }
            }

            await _accreditationSiteMaterialService.UpdateNonWasteInputs(viewModel);

            if (saveButton == SaveButton.SaveAndComeBack)
            {
                PopulateBackModel(_siteNonWasteInputsRouteName);

                // this is all the data we require to save for come back later
                await _saveAndComeBackService.AddSaveAndComeBack(
                    viewModel.Id,
                    _httpContextAccessor.HttpContext.GetRouteData().Values);
                return View("_ApplicationSaved");
            }
            else
            {
                return RedirectToRoute(
                    SiteMaterialOutputsRouteName,
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
                {
                    return NotFound();
                }

                // if waste last year is true then return the MaterialOutputs view
                if (materialOutputsViewModel.WasteLastYear == true)
                {
                    return View(MaterialOutputsLastYearView, materialOutputsViewModel);
                }
                else
                {
                    // otherwise return the annual outputs
                    return View(MaterialOutputsEstimatedView, materialOutputsViewModel);
                }
            }
            else
            {
                return NotFound();
            }
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
                PopulateBackModel(_siteNonWasteInputsRouteName);

                // this is all the data we require to save for come back later
                await _saveAndComeBackService.AddSaveAndComeBack(
                    viewModel.Id,
                    _httpContextAccessor.HttpContext.GetRouteData().Values);
                return View("_ApplicationSaved");
            }
            else
            {
                return RedirectToRoute(
                    ProductsProducedRouteName,
                    new
                    {
                        viewModel.Id,
                        viewModel.MaterialId
                    });
            }
        }

        /// <summary>
        /// Get method for displaying the products produced (Both estimated and
        /// last calender year
        /// </summary>
        /// <param name="id">Accreditation id</param>
        /// <param name="materialId">material id</param>
        /// <returns>View result or other relevant result (NotFound, etc)</returns>
        [HttpGet("ProductsProduced", Name = "ProductsProduced")]
        public async Task<IActionResult> ProductsProduced(
            Guid? id,
            Guid? materialId)
        {
            if (id.HasValue &&
                materialId.HasValue)
            {
                PopulateBackModel(SiteMaterialOutputsRouteName);

                var productsProducedViewModel = await _accreditationSiteMaterialService.GetProductsProduced(
                    id.Value,
                    materialId.Value);

                // if waste last year has not been set, then the user should not
                // be on this page
                if (productsProducedViewModel.WasteLastYear == null)
                {
                    return NotFound();
                }

                // if waste last year is true then return the MaterialOutputs view
                if (productsProducedViewModel.WasteLastYear == true)
                {
                    return View(ProductsProducedLastYearView, productsProducedViewModel);
                }
                else
                {
                    // otherwise return the annual outputs
                    return View(ProductsProducedEstimatedView, productsProducedViewModel);
                }
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
        [HttpPost("ProductsProduced")]
        public async Task<IActionResult> ProductsProduced(
            ProductsProducedViewModel viewModel,
            SaveButton saveButton)
        {
            PopulateBackModel(SiteMaterialOutputsRouteName);

            if (saveButton == SaveButton.AddRow)
            {
                // We're adding a new row, therefore we do not want to perform any validation
                ModelState.Clear();

                // user is adding a new row (without javascript) and therefore we need to
                // return the view with a new row added
                if (viewModel.RowsToDisplay(_initialTypeTonnesRows) < _maximumMultiLineRecordNumber)
                {
                    viewModel.RowsToAdd++;
                }

                return View(
                    viewModel.WasteLastYear.Value ? ProductsProducedLastYearView : ProductsProducedEstimatedView,
                    viewModel);
            }

            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                MaterialOutputsLastYearResources.MaterialsNotProcessedBlank,
                MaterialOutputsLastYearResources.ContaminentsBlank,
                MaterialOutputsLastYearResources.ProcessLossBlank))
            {
                if (viewModel.WasteLastYear == true)
                {
                    return View(ProductsProducedLastYearView, viewModel);
                }
                else
                {
                    return View(ProductsProducedEstimatedView, viewModel);
                }
            }

            await _accreditationSiteMaterialService.UpdateProductsProduced(viewModel);

            if (saveButton == SaveButton.SaveAndComeBack)
            {
                PopulateBackModel(_siteNonWasteInputsRouteName);

                // this is all the data we require to save for come back later
                await _saveAndComeBackService.AddSaveAndComeBack(
                    viewModel.Id,
                    _httpContextAccessor.HttpContext.GetRouteData().Values);
                return View("_ApplicationSaved");
            }
            else
            {
                return RedirectToRoute(
                    AuthorityRouteName,
                    new
                    {
                        viewModel.Id,
                        viewModel.MaterialId
                    });
            }
        }

        /// <summary>
        /// Stubbed method for entering processing capacity
        /// </summary>
        /// <returns>Not found view for the moment</returns>
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
            {
                return NotFound();
            }
        }

        [HttpPost("WasteLastYear")]
        public async Task<IActionResult> WasteLastYear(
            ReprocessedWasteLastYearViewModel viewModel,
            SaveButton saveButton)
        {
            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                PermitExemptionResources.ErrorMessage))
            {
                return View(viewModel);
            }

            await _accreditationSiteMaterialService.UpdateReprocessedWasteLastYear(viewModel);

            if (saveButton == SaveButton.SaveAndContinue)
            {
                return RedirectToRoute(
                    NonWasteInputsRouteName,
                    new
                    {
                        id = viewModel.Id,
                        viewModel.MaterialId
                    });
            }

            // this is all the data we require to save for come back later
            await _saveAndComeBackService.AddSaveAndComeBack(
                viewModel.Id,
                _httpContextAccessor.HttpContext.GetRouteData().Values);
            return View("_ApplicationSaved");
        }

        [HttpGet("Authority", Name = "Authority")]
        public Task<IActionResult> Authority()
        {
            return null;
        }
    }
}