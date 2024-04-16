namespace EPR.Accreditation.Portal.Controllers
{
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for the accreditation site
    /// </summary>
    [Route("[controller]/{id}/Site/")]
    public class AccreditationSiteController : Controller
    {
        private readonly IAccreditationSiteService _accreditationSiteService;
        private readonly ISaveAndComeBackService _saveAndComeBackService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BackPageViewModel _backPageViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccreditationSiteController"/> class.
        /// </summary>
        /// <param name="accreditationSiteService">Dependency injection for site service</param>
        /// <param name="saveAndComeBackService">Dependency injection for save and come back service </param>
        /// <param name="httpContextAccessor">Dependency injection for conext accessor</param>
        /// <param name="backPageViewModel">Dependency injection for the back page view model</param>
        /// <exception cref="ArgumentNullException"> Checks if any are null</exception>
        public AccreditationSiteController(
            IAccreditationSiteService accreditationSiteService,
            ISaveAndComeBackService saveAndComeBackService,
            IHttpContextAccessor httpContextAccessor,
            BackPageViewModel backPageViewModel)
        {
            _accreditationSiteService = accreditationSiteService ?? throw new ArgumentNullException(nameof(accreditationSiteService));
            _saveAndComeBackService = saveAndComeBackService ?? throw new ArgumentNullException(nameof(saveAndComeBackService));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _backPageViewModel = backPageViewModel;
        }

        /// <summary>
        /// Gets the exemption references view
        /// </summary>
        /// <param name="id">Accreditation ID</param>
        /// <returns>The view</returns>
        [HttpGet("ExemptionReferences")]
        public async Task<IActionResult> ExemptionReferences(Guid? id)
        {
            _backPageViewModel.Url = $"/Accreditation/{id}/PermitExemption";

            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _accreditationSiteService.GetExemptionReferencesViewModel(id.Value);

            return View(viewModel);
        }

        /// <summary>
        /// Submits the exemption reference(s) value(s)
        /// </summary>
        /// <param name="viewModel">The pertinent view model</param>
        /// <param name="saveButton">Enum of save button</param>
        /// <returns>Task completed asynchronously</returns>
        [HttpPost("ExemptionReferences")]
        public async Task<IActionResult> ExemptionReferences(
            ExemptionReferencesViewModel viewModel,
            SaveButton saveButton)
        {
            if (!ModelState.IsValid)
            {
                _backPageViewModel.Url = $"/Accreditation/{viewModel.Id}/PermitExemption";
                return View(viewModel);
            }

            await _accreditationSiteService.UpdateExemptionReferences(viewModel);

            if (saveButton == SaveButton.SaveAndContinue)
            {
                return RedirectToAction("HowManyTonnes", "Accreditation");
            }

            // this is all the data we require to save for come back later
            await _saveAndComeBackService.AddSaveAndComeBack(
                viewModel.Id,
                _httpContextAccessor.HttpContext.GetRouteData().Values);
            return View("_ApplicationSaved");
        }
    }
}
