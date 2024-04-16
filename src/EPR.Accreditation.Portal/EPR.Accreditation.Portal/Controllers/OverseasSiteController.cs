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

    [Route("Accreditation/{accreditationExternalId}/[controller]/")]
    public class OverseasSiteController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccreditationService _accreditationService;
        private readonly ISaveAndComeBackService _saveAndComeBackService;
        private readonly BackPageViewModel _backPageViewModel;
        private IUrlHelperWrapper _urlHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OverseasSiteController"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">Injected http context accessor service.</param>
        /// <param name="saveAndComeBackService">Injected save and come back service.</param>
        /// <param name="accreditationService">Injected accreditation service.</param>
        /// <param name="urlHelper">Url helper service.</param>
        /// <param name="backPageViewModel">Handles back page control.</param>
        public OverseasSiteController(
            IHttpContextAccessor httpContextAccessor,
            ISaveAndComeBackService saveAndComeBackService,
            IAccreditationService accreditationService,
            IUrlHelperWrapper urlHelper,
            BackPageViewModel backPageViewModel)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _saveAndComeBackService = saveAndComeBackService ?? throw new ArgumentNullException(nameof(saveAndComeBackService));
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
            _backPageViewModel = backPageViewModel;
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
            if (accreditationExternalId.HasValue && siteExternalId.HasValue)
            {
                var overseasSiteOutputsViewModel = await _accreditationService.GetOverseasReprocessingSiteOutputs(
                    accreditationExternalId.Value,
                    siteExternalId.Value);

                return View(overseasSiteOutputsViewModel);
            }

            return BadRequest("Missing over seas site ids");
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

            await _accreditationService.UpdateOverseasReprocessingSiteOutputs(overseasReprocessingSiteOutputsViewModel);

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