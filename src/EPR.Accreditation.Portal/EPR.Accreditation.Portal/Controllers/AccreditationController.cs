namespace EPR.Accreditation.Portal.Controllers
{
    using EPR.Accreditation.Portal.Constants;
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Extensions;
    using EPR.Accreditation.Portal.Helpers.Interfaces;
    using EPR.Accreditation.Portal.Options;
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Options;

    [Route("[controller]/{id}")]
    public class AccreditationController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccreditationService _accreditationService;
        private readonly IWastePermitService _wastePermitService;
        private readonly ISaveAndComeBackService _saveAndComeBackService;
        private readonly BackPageViewModel _backPageViewModel;
        private readonly IUrlHelperWrapper _urlHelper;
        private readonly IOptions<AppSettingsConfigOptions> _appSettings;

        public AccreditationController(
            IHttpContextAccessor httpContextAccessor,
            IWastePermitService wastePermitService,
            ISaveAndComeBackService saveAndComeBackService,
            IAccreditationService accreditationService,
            IUrlHelperWrapper urlHelper,
            BackPageViewModel backPageViewModel,
            IOptions<AppSettingsConfigOptions> appSettings)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _wastePermitService = wastePermitService ?? throw new ArgumentNullException(nameof(wastePermitService));
            _saveAndComeBackService = saveAndComeBackService ?? throw new ArgumentNullException(nameof(saveAndComeBackService));
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
            _backPageViewModel = backPageViewModel;
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
        }

        [HttpGet("PermitExemption")]
        public async Task<IActionResult> CheckWastePermitExemption(Guid? id)
        {
            _backPageViewModel.Url = _urlHelper.ActionLink("ApplyForAccreditation", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _wastePermitService.GetPermitExemptionViewModel(id.Value);

            return View(viewModel);
        }

        [HttpPost("PermitExemption")]
        public async Task<IActionResult> CheckWastePermitExemption(
            PermitExemptionViewModel viewModel,
            SaveButton saveButton)
        {
            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                PermitExemptionResources.ErrorMessage))
            {
                return View(viewModel);
            }

            await _wastePermitService.UpdatePermitExemption(viewModel);

            if (saveButton == SaveButton.SaveAndContinue &&
                viewModel.HasPermitExemption.Value == true)
            {
                return RedirectToAction("ExemptionReferences", "Accreditation");
            }
            else if (saveButton == SaveButton.SaveAndContinue &&
                viewModel.HasPermitExemption.Value == false)
            {
                return RedirectToAction("AuthorityToIssues", "Accreditation");
            }

            // this is all the data we require to save for come back later
            await _saveAndComeBackService.AddSaveAndComeBack(
                viewModel.Id,
                _httpContextAccessor.HttpContext.GetRouteData().Values);
            return View("_ApplicationSaved");
        }

        [HttpGet("WasteLicensesAndPermits")]
        public async Task<IActionResult> WasteLicensesAndPermits(Guid? id)
        {
            _backPageViewModel.Url = _urlHelper.ActionLink("ApplyForAccreditation", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _accreditationService.GetWastePermitViewModel(id.Value);

            return View(viewModel);
        }

        [HttpPost("WasteLicensesAndPermits")]
        public async Task<IActionResult> WasteLicensesAndPermits(WasteLicensesAndPermitsViewModel viewModel, SaveButton saveButton)
        {
            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                PermitExemptionResources.ErrorMessage))
            {
                return View(viewModel);
            }

            await _accreditationService.SaveWastePermit(viewModel);

            if (saveButton == SaveButton.SaveAndComeBack)
            {
                // this is all the data we require to save for come back later
                await _saveAndComeBackService.AddSaveAndComeBack(
                    viewModel.Id,
                    Request.HttpContext.GetRouteData().Values);

                return View("_ApplicationSaved");
            }
            else
            {
                return RedirectToAction(
                    "PermitExemption",
                    "Accreditation",
                    new
                    {
                        viewModel.Id
                    });
            }
        }

        [HttpGet]
        [ActionName("OperatorType")]
        public async Task<IActionResult> OperatorType(Guid? id)
        {
            if (id.HasValue)
            {
                var operatorType = await _accreditationService.GetOperatorType(id.Value);

                return View(operatorType);
            }

            return View(new OperatorTypeViewModel());
        }

        [HttpPost]
        [ActionName("OperatorType")]
        public async Task<IActionResult> OperatorType(OperatorTypeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var externalId = await _accreditationService.CreateAccreditation(vm);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Site/{siteId}/Material/{materialId}/TaskList", Name = "TaskList")]
        public async Task<IActionResult> TaskList(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            if (id != null &&
                siteId != null &&
                materialId != null)
            {
                TaskListViewModel model = await _accreditationService.GetTaskList(
                    id.Value,
                    siteId.Value,
                    materialId.Value);
                return View(model);
            }

            return NotFound();
        }

        [HttpGet("Overseas")]
        public async Task<IActionResult> Overseas(
            Guid? id)
        {
            return View("overseas");
        }

        [HttpGet("Upload")]
        public async Task<IActionResult> Upload(Guid id)
        {
            return View("upload");
        }

        [HttpGet("CheckYourAnswers")]
        public async Task<IActionResult> CheckYourAnswers(Guid? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            CheckYourAnswersViewModel vm = await _accreditationService.CheckYourAnswers(id.Value);
            return View(vm);
        }

        [HttpPost("CheckYourAnswers")]
        public async Task<IActionResult> CheckYourAnswers(CheckYourAnswersViewModel checkYourAnswersViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("CheckYourAnswers", new { id = checkYourAnswersViewModel.Id });
            }

            return RedirectToAction("Index", "Home");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Handle redirection to CheckYourAnswers if this is where we originally came from
            if (context.HttpContext.Request.Query.ContainsKey(Strings.QueryStrings.ReturnToAnswers) &&
                context.HttpContext.Request.Query[Strings.QueryStrings.ReturnToAnswers] == Strings.QueryStrings.ReturnToAnswersYes &&
                context.Result is RedirectToActionResult)
            {
                var id = (context.Result as RedirectToActionResult).RouteValues["Id"].ToString();
                context.Result = RedirectToAction("CheckYourAnswers", "Accreditation", new { id });
            }

            base.OnActionExecuted(context);
        }

        [HttpGet("Site/{siteId}/Material/{materialId}/TaskListSite", Name = "TaskListSite")]
        public async Task<IActionResult> TaskListSite(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            if (id != null &&
                siteId != null &&
                materialId != null)
            {
                TaskListViewModel model = await _accreditationService.GetTaskList(
                    id.Value,
                    siteId.Value,
                    materialId.Value);
                return View(model);
            }

            return NotFound();
        }

        [HttpGet("SiteAddress")]
        public async Task<IActionResult> SiteAddress(
            Guid? id)
        {
            return NotFound();
        }

        [HttpGet("WasteCarrierRegistrationNumber")]
        public async Task<IActionResult> WasteCarrierRegistrationNumber(
            Guid? id)
        {
            return NotFound();
        }

        [HttpGet("WasteManagementPermitNumber")]
        public async Task<IActionResult> WasteManagementPermitNumber(
            Guid? id)
        {
            return NotFound();
        }

        [HttpGet("PartABCReferenceNumber")]
        public async Task<IActionResult> PartABCReferenceNumber(
            Guid? id)
        {
            return NotFound();
        }

        [HttpGet("DischargeConsentNumber")]
        public async Task<IActionResult> DischargeConsentNumber(
            Guid? id)
        {
            return NotFound();
        }

        [HttpGet("ExemptionReference")]
        public async Task<IActionResult> ExemptionReference(
            Guid? id)
        {
            return NotFound();
        }

        [HttpGet("RejectedWastePlans")]
        public async Task<IActionResult> RejectedWastePlans(
            Guid? id)
        {
            return NotFound();
        }

        [HttpGet("OverseasAgentChoice", Name = "OverseasAgentChoice")]
        public async Task<IActionResult> OverseasAgentChoice(
            Guid? id)
        {
            return NotFound();
        }

        [HttpGet("PrnTonnesPlanned")]
        [Route("PrnTonnesPlanned")]
        public async Task<IActionResult> PrnTonnesPlanned(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            PrnTonnesPlannedViewModel vm = await _accreditationService.GetPrnTonnesPlanned(id.Value);
            return View(vm);
        }

        [HttpPost("PrnTonnesPlanned")]
        [Route("PrnTonnesPlanned")]
        public async Task<IActionResult> PrnTonnesPlanned(Guid? id, PrnTonnesPlannedViewModel vm)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            vm.PrnPlannedTonnesFee = vm.PrnPlannedTonnesType == Common.Enums.PrnPlannedTonnesType.Upto ?
                _appSettings.Value.PrnTonnageUpto400Fee :
                _appSettings.Value.PrnTonnageOver400Fee.Value;
            await _accreditationService.UpdatePrnTonnesPlanned(id.Value, vm);

            return RedirectToAction("Declaration", new { id = id.Value });
        }

        [HttpGet("Declaration")]
        [Route("Declaration")]
        public async Task<IActionResult> Declaration(
            Guid? id)
        {
            return NotFound();
        }
    }
}
