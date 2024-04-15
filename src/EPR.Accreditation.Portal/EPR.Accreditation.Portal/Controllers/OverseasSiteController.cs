namespace EPR.Accreditation.Portal.Controllers
{
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Helpers.Interfaces;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [Route("Accreditation/{accreditationExternalId}/[controller]")]
    public class OverseasSiteController : Controller
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccreditationService _accreditationService;
        protected readonly ISaveAndComeBackService _saveAndComeBackService;
        protected readonly BackPageViewModel _backPageViewModel;
        protected IUrlHelperWrapper _urlHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OverseasSiteController"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">Injected http context accessor service.</param>
        /// <param name="saveAndComeBackService">Injected save and come back service.</param>
        /// <param name="accreditationService">Injected accreditation service.</param>
        /// <param name="backPageViewModel"></param>
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
        [ActionName("Outputs")]
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
        /// <param name="accreditationExternalId">Accreditation external Id.</param>
        /// <param name="overseasReprocessingSiteOutputsViewModel">View model containing data to be saved.</param>
        /// <param name="saveButton">Determines the next step in the journey.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut("Outputs")]
        [ActionName("Outputs")]
        public async Task<IActionResult> OverseasSiteOutputs(
            Guid accreditationExternalId,
            [FromBody] OverseasReprocessingSiteOutputsViewModel overseasReprocessingSiteOutputsViewModel,
            SaveButton saveButton)
        {
            if (!ModelState.IsValid)
                return View(overseasReprocessingSiteOutputsViewModel);

            await _accreditationService.UpdateOverseasReprocessingSiteOutputs(
                accreditationExternalId,
                overseasReprocessingSiteOutputsViewModel);

            if (saveButton == SaveButton.SaveAndContinue)
            {
                if (string.IsNullOrWhiteSpace(overseasReprocessingSiteOutputsViewModel.Outputs))
                {
                    return RedirectToAction("Outputs", "OverseasSite", new {});
                }
                else
                {
                    return RedirectToAction("RejectedWastePlans", "Accreditation");
                }
            }

            // this is all the data we require to save and come back later
            await _saveAndComeBackService.AddSaveAndComeBack(
                accreditationExternalId,
                _httpContextAccessor.HttpContext.GetRouteData().Values);

            return View("_ApplicationSaved");
        }
    }
}