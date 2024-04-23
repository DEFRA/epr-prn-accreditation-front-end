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
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [Route("Accreditation/{id}/OverseasSite/{siteId}/Material/{materialId}")]
    [ServiceFilter(typeof(WasteTypeActionFilter))]
    public class OverseasSiteMaterialController : BaseSiteController
    {
        private readonly int _initialWasteCodesRows;
        private readonly int _maximumMultiLineRecordNumber;

        public OverseasSiteMaterialController(
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettingsConfigOptions> appSettingsConfiguration,
            IUrlHelperWrapper urlHelper,
            IAccreditationSiteMaterialService accreditationSiteMaterialService,
            ISaveAndComeBackService saveAndComeBackService,
            BackPageViewModel backPageViewModel)
            : base(
                  httpContextAccessor,
                  urlHelper,
                  accreditationSiteMaterialService,
                  saveAndComeBackService,
                  backPageViewModel,
                  SiteType.OverseasSite)
        {
            _siteProcessingCapacityRouteName = "OverseasSiteProcessingCapacity";
            _siteChooseMaterialRouteName = "OverseasSiteChooseMaterial";

            if (appSettingsConfiguration?.Value?.MaximumMultiLineRecordNumber == null)
            {
                throw new ArgumentNullException(nameof(appSettingsConfiguration.Value.MaximumMultiLineRecordNumber));
            }

            if (appSettingsConfiguration?.Value?.InitialTypeTonnesRows == null)
            {
                throw new ArgumentNullException(nameof(appSettingsConfiguration.Value.InitialTypeTonnesRows));
            }

            _initialWasteCodesRows = appSettingsConfiguration.Value.InitialTypeTonnesRows.Value;
            _maximumMultiLineRecordNumber = appSettingsConfiguration.Value.MaximumMultiLineRecordNumber.Value;
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

        /// <summary>
        /// STUB for "Do you want to upload another piece of evidence
        /// Not sure if this is needed for Sites as well
        /// </summary>
        /// <param name="id"></param>
        /// <param name="siteId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [HttpGet("UploadMoreEvidence", Name = "UploadMoreEvidence")]
        public async Task<IActionResult> UploadMoreEvidence(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            return NotFound();
        }

        /// <summary>
        /// Gets the Waste Description view. This is for
        /// exporters only, so if the accreditation is a reprocessor,
        /// we should geta NotFound
        /// </summary>
        /// <param name="id">The id of the accreditation</param>
        /// <param name="siteId">The site id of the overseas sites</param>
        /// <param name="materialId">The id of the material</param>
        /// <returns>An appropriate IActionResult</returns>
        [HttpGet("WasteDescription", Name = "WasteDescription")]
        public async Task<IActionResult> WasteDescription(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            if (id.HasValue &&
                siteId.HasValue &&
                materialId.HasValue)
            {
                PopulateBackModel("UploadMoreEvidence");

                var viewModel = await _accreditationSiteMaterialService.GetWasteDescriptionCodeViewModel(
                    id.Value,
                    siteId.Value,
                    materialId.Value);

                return View(viewModel);
            }

            return NotFound();
        }

        /// <summary>
        /// Handler for the POST method for waste description codes
        /// </summary>
        /// <param name="viewModel">The waste description codes view model</param>
        /// <param name="saveButton">The value representing which save button has been pressed</param>
        /// <returns>An async IActionResult represengint the relevant view or action for the input data</returns>
        [HttpPost("WasteDescription")]
        public async Task<IActionResult> WasteDescription(
            WasteDescriptionCodeViewModel viewModel,
            SaveButton saveButton)
        {
            if (saveButton == SaveButton.AddRow)
            {
                // We're adding a new row, therefore we do not want to perform any validation
                ModelState.Clear();

                // user is adding a new row (without javascript) and therefore we need to
                // return the view with a new row added
                if (viewModel.RowsToDisplay(_initialWasteCodesRows) < _maximumMultiLineRecordNumber)
                {
                    viewModel.RowsToAdd++;
                }

                return View(viewModel);
            }

            PopulateBackModel("UploadMoreEvidence");

            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                WasteDescriptionCodeResources.AtLeastOneEntryRequired))
            {
                return View(viewModel);
            }

            await _accreditationSiteMaterialService.UpdateWasteDescriptionCodeViewModel(viewModel);

            if (saveButton == SaveButton.SaveAndComeBack)
            {
                await _saveAndComeBackService.AddSaveAndComeBack(
                    viewModel.Id,
                    _httpContextAccessor.HttpContext.Request.RouteValues);

                return View("_ApplicationSaved");
            }

            return RedirectToAction(
                "OverseasAgentChoice",
                viewModel.Id);
        }
    }
}