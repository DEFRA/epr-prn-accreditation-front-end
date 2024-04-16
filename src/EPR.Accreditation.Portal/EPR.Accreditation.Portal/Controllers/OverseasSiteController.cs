namespace EPR.Accreditation.Portal.Controllers
{
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Extensions;
    using EPR.Accreditation.Portal.Helpers.Interfaces;
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [Route("Accreditation/{id}/[controller]/{overseasSiteId}")]
    public class OverseasSiteController : Controller
    {
        private readonly IOverseasSiteService _overseasSiteService;
        private readonly ISaveAndComeBackService _saveAndComeBackService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelperWrapper _urlHelper;
        private readonly BackPageViewModel _backPageViewModel;

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

        [HttpGet("ReprocessorDetails")]
        public async Task<IActionResult> ReprocessorDetails(
            Guid? id,
            Guid? overseasSiteId)
        {
            _backPageViewModel.Url = _urlHelper.ActionLink("AddOverseasReprocessingSites", "OverseasSite");

            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _overseasSiteService.GetReprocessorDetailsViewModel(
                id.Value,
                overseasSiteId.Value);

            return View(viewModel);
        }

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
                return RedirectToAction("ExemptionReferences", "Accreditation");
            }

            // this is all the data we require to save for come back later
            await _saveAndComeBackService.AddSaveAndComeBack(
                viewModel.Id,
                _httpContextAccessor.HttpContext.GetRouteData().Values);
            return View("_ApplicationSaved");
        }
    }
}
