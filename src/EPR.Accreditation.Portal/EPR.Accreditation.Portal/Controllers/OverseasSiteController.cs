namespace EPR.Accreditation.Portal.Controllers
{
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Extensions;
    using EPR.Accreditation.Portal.Helpers.Interfaces;
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for the overseas site
    /// </summary>
    [Route("Accreditation/{id}/[controller]/{overseasSiteId}")]
    public class OverseasSiteController : BaseController
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
            : base(httpContextAccessor, urlHelper, backPageViewModel)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _saveAndComeBackService = saveAndComeBackService ?? throw new ArgumentNullException(nameof(saveAndComeBackService));
            _overseasSiteService = overseasSiteService ?? throw new ArgumentNullException(nameof(overseasSiteService));
            _backPageViewModel = backPageViewModel;
        }

        /// <summary>
        /// STUBBED method for Create overseas site
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/Accreditation/{id}/[controller]/Create", Name = "CreateOverseasSite")]
        public async Task<IActionResult> CreateSite(Guid? id)
        {
            return NotFound();
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

            return RedirectToAction("PersonWeCanContact", "Accreditation");
        }
    }
}
