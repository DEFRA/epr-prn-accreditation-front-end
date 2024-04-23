namespace EPR.Accreditation.Portal.Controllers
{
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Extensions;
    using EPR.Accreditation.Portal.Helpers.Interfaces;
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.Services.Accreditation;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for the overseas site
    /// </summary>
    [Route("Accreditation/{id}/[controller]/{overseasSiteId}")]
    public class OverseasSiteController : Controller
    {
        private readonly IOverseasSiteService _overseasSiteService;
        private readonly ISaveAndComeBackService _saveAndComeBackService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelperWrapper _urlHelper;
        private readonly BackPageViewModel _backPageViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="OverseasSiteController"/> class.
        /// </summary>
        /// <param name="overseasSiteService">Dependency injection for the overseas site service</param>
        /// <param name="saveAndComeBackService">Dependency injection for the save and come back service</param>
        /// <param name="httpContextAccessor">Dependency injection for the context accessor</param>
        /// <param name="urlHelper">Dependency injection for the URL helper</param>
        /// <param name="backPageViewModel">Dependency injection for the back link view model</param>
        /// <exception cref="ArgumentNullException">Checks if any are null</exception>
        public OverseasSiteController(
            IOverseasSiteService overseasSiteService,
            ISaveAndComeBackService saveAndComeBackService,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelperWrapper urlHelper,
            BackPageViewModel backPageViewModel)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _saveAndComeBackService = saveAndComeBackService ?? throw new ArgumentNullException(nameof(saveAndComeBackService));
            _overseasSiteService = overseasSiteService ?? throw new ArgumentNullException(nameof(overseasSiteService));
            _backPageViewModel = backPageViewModel;
        }

        /// <summary>
        /// Gets the reprocesesor details view
        /// </summary>
        /// <param name="id">Accreditation ID</param>
        /// <param name="overseasSiteId">Overseas site ID</param>
        /// <returns>The view result</returns>
        [HttpGet("ReprocessorDetails")]
        public async Task<IActionResult> ReprocessorDetails(
            Guid? id,
            Guid? overseasSiteId)
        {
            _backPageViewModel.Url = _urlHelper.ActionLink("AddOverseasReprocessingSites", "OverseasSite");

            if (id == null ||
                overseasSiteId == null)
            {
                return NotFound();
            }

            var viewModel = await _overseasSiteService.GetReprocessorDetailsViewModel(
                id.Value,
                overseasSiteId.Value);

            return View(viewModel);
        }

        /// <summary>
        /// Submits the reprocessor details
        /// </summary>
        /// <param name="viewModel">The relevant view model</param>
        /// <param name="saveButton">Enum of the save button</param>
        /// <returns>Task completed asynchronously</returns>
        [HttpPost("ReprocessorDetails")]
        public async Task<IActionResult> ReprocessorDetails(
            ReprocessorDetailsViewModel viewModel,
            SaveButton saveButton)
        {
            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                ReprocessorDetailsResources.ErrorOrgName,
                ReprocessorDetailsResources.ErrorCountry,
                ReprocessorDetailsResources.Address))
            {
                viewModel = await _overseasSiteService.GetReprocessorDetailsViewModel(
                    viewModel.Id,
                    viewModel.OverseasSiteId);

                return View(viewModel);
            }

            await _overseasSiteService.UpdateReprocessorDetails(viewModel);

            if (saveButton == SaveButton.SaveAndContinue)
            {
                return RedirectToAction("PersonWeCanContact", "Accreditation");
            }

            // this is all the data we require to save for come back later
            await _saveAndComeBackService.AddSaveAndComeBack(
                viewModel.Id,
                _httpContextAccessor.HttpContext.GetRouteData().Values);
            return View("_ApplicationSaved");
        }

        /// <summary>
        /// Gets the Outputs value for the given accreditation and site.
        /// </summary>
        /// <param name="accreditationExternalId">Accreditation external Id.</param>
        /// <param name="siteExternalId">Site external Id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet("{siteExternalId}/Outputs")]
        public async Task<IActionResult> OverseasSiteOutputs(Guid? accreditationExternalId, Guid? siteExternalId)
        {
            if (accreditationExternalId == null || siteExternalId == null)
            {
                return NotFound();
            }

            var overseasSiteOutputsViewModel = await _overseasSiteService.GetOverseasReprocessingSiteOutputs(
                accreditationExternalId.Value,
                siteExternalId.Value);

            return View(overseasSiteOutputsViewModel);
        }

        /// <summary>
        /// Updates the Outputs value for the given accreditation and site.
        /// </summary>
        /// <param name="overseasReprocessingSiteOutputsViewModel">View model containing data to be saved.</param>
        /// <param name="saveButton">Determines the next step in the journey.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost("{siteExternalId}/Outputs")]
        public async Task<IActionResult> OverseasSiteOutputs(
            OverseasReprocessingSiteOutputsViewModel overseasReprocessingSiteOutputsViewModel,
            SaveButton saveButton)
        {
            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                OverseasSiteOutputsResources.NoOutputsSupplied))
            {
                return View(overseasReprocessingSiteOutputsViewModel);
            }

            await _overseasSiteService.UpdateOverseasReprocessingSiteOutputs(overseasReprocessingSiteOutputsViewModel);

            if (saveButton == SaveButton.SaveAndContinue && string.IsNullOrWhiteSpace(overseasReprocessingSiteOutputsViewModel.Outputs))
            {
                return RedirectToAction("OverseasSiteOutput", "OverseasSite");
            }
            else if (saveButton == SaveButton.SaveAndContinue && !string.IsNullOrWhiteSpace(overseasReprocessingSiteOutputsViewModel.Outputs))
            {
                return RedirectToAction("RejectedWastePlans", "Accreditation");
            }

            await _saveAndComeBackService.AddSaveAndComeBack(
                overseasReprocessingSiteOutputsViewModel.Id,
                _httpContextAccessor.HttpContext.GetRouteData().Values);

            return View("_ApplicationSaved");
        }
    }
}
