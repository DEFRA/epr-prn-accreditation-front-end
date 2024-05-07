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
    using EPR.Accreditation.Portal.ViewModels.Accreditation;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Main accreditator controller. Anything specific to just the accreditator should be handled here
    /// </summary>
    [Route("[controller]/{id}")]
    public class AccreditationController : BaseController
    {
        private const string TaskListRouteName = "TaskList";
        private const string RegulatorContactRouteName = "RegulatorContact";
        private const string LegalDocumentsRouteName = "LegalDocuments";
        private const string LegalDocumentsAddressRouteName = "LegalDocumentsAddress";
        private const string CreateOverseasSiteRouteName = "CreateOverseasSite";
        private readonly IAccreditationService _accreditationService;
        private readonly IWastePermitService _wastePermitService;
        private readonly ISiteService _siteService;
        private readonly IOptions<AppSettingsConfigOptions> _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccreditationController"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The IHttpContextAccessor relevant to the current request</param>
        /// <param name="wastePermitService">The IWastePermitService instance</param>
        /// <param name="accreditationService">The IAccreditationService instance</param>
        /// <param name="siteSerivce">The ISiteService instance</param>
        /// <param name="urlHelper">The IUrlHelperWrapper which wraps IUrlHelper</param>
        /// <param name="backPageViewModel">View model for the back page link</param>
        /// <param name="appSettings">The app settings configuratrion</param>
        /// <exception cref="ArgumentNullException">If any parameters are null, this exception is thrown</exception>
        public AccreditationController(
            IHttpContextAccessor httpContextAccessor,
            IWastePermitService wastePermitService,
            IAccreditationService accreditationService,
            ISiteService siteSerivce,
            IUrlHelperWrapper urlHelper,
            BackPageViewModel backPageViewModel,
            IOptions<AppSettingsConfigOptions> appSettings)
            : base(
                  httpContextAccessor,
                  urlHelper,
                  backPageViewModel)
        {
            _wastePermitService = wastePermitService ?? throw new ArgumentNullException(nameof(wastePermitService));
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
            _siteService = siteSerivce ?? throw new ArgumentNullException(nameof(siteSerivce));
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
            var actionResult = default(ActionResult);

            if (saveButton == SaveButton.SaveAndContinue &&
                viewModel.HasPermitExemption.Value == true)
            {
                actionResult = RedirectToAction("ExemptionReferences", "Accreditation");
            }
            else if (saveButton == SaveButton.SaveAndContinue &&
                viewModel.HasPermitExemption.Value == false)
            {
                actionResult = RedirectToAction("AuthorityToIssues", "Accreditation");
            }

            if (actionResult != null)
            {
                return actionResult;
            }

            return new EmptyResult();
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

            return RedirectToAction(
                "PermitExemption",
                "Accreditation",
                new
                {
                    viewModel.Id
                });
        }

        [HttpGet("OperatorType")]
        public async Task<IActionResult> OperatorType(Guid? id)
        {
            if (id.HasValue)
            {
                var operatorType = await _accreditationService.GetOperatorType(id.Value);

                return View(operatorType);
            }

            return View(new OperatorTypeViewModel());
        }

        [HttpPost("OperatorType")]
        public async Task<IActionResult> OperatorType(OperatorTypeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var externalId = await _accreditationService.CreateAccreditation(vm);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("TaskList", Name = "TaskList")]
        public async Task<IActionResult> TaskList(Guid id)
        {
            PopulateBackModel(TaskListRouteName);

            TaskListViewModel model = await _accreditationService.GetTaskList(id);
            return View(model);
        }

        [HttpGet("TaskListSite", Name = "TaskListSite")]
        public async Task<IActionResult> TaskListSite(Guid id)
        {
            PopulateBackModel(TaskListRouteName);

            TaskListViewModel model = await _accreditationService.GetTaskList(id);
            return View(model);
        }

        /// <summary>
        /// Returns the Overseas reprocessors view
        ///
        /// This may need moving to a different controller in the long term. It'll do for now
        /// though.
        /// </summary>
        /// <param name="id">The id of the accreditation</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous IActionResult.</returns>
        [HttpGet("Material/{materialId}/OverseasReprocessors")]
        public async Task<IActionResult> OverseasReprocessors(
            Guid? id)
        {
            PopulateBackModel(TaskListRouteName);

            if (id.HasValue)
            {
                if (await _accreditationService.IsExporter(id.Value))
                {
                    return View();
                }
            }

            return NotFound();
        }

        /// <summary>
        /// Returns the Overseas reprocessors view
        ///
        /// This may need moving to a different controller in the long term. It'll do for now
        /// though.
        /// </summary>
        /// <param name="materialId">the material id that this journey is part of</param>
        /// <param name="viewModel">The view model for the page</param>
        /// <param name="saveButton">The save button selection</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous IActionResult.</returns>
        [HttpPost("Material/{materialId}/OverseasReprocessors")]
        public async Task<IActionResult> OverseasReprocessors(
            Guid? materialId,
            OverseasReprocessorViewModel viewModel,
            SaveButton saveButton)
        {
            if (materialId == null)
            {
                return NotFound();
            }

            if (!await _accreditationService.IsExporter(viewModel.Id))
            {
                return NotFound();
            }

            PopulateBackModel(TaskListRouteName);

            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                WasteDescriptionCodeResources.AtLeastOneEntryRequired))
            {
                return View(viewModel);
            }

            if (viewModel.AddOverseasReprocessor.Value)
            {
                // redirect to Add overeas reprocessing site
                return RedirectToRoute(
                    CreateOverseasSiteRouteName,
                    new
                    {
                        id = viewModel.Id
                    });
            }

            // redirect back to the task list
            return RedirectToRoute(
                TaskListRouteName,
                new
                {
                    id = viewModel.Id,
                    materialId = materialId
                });
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

        [HttpGet("Site", Name = "Site")]
        public async Task<IActionResult> SiteAddress(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _siteService.GetSiteAddressViewModel(id.Value);

            return View(viewModel);
        }

        [HttpPost("Site")]
        public async Task<IActionResult> SiteAddress(SiteAddressViewModel viewModel, SaveButton saveButton)
        {
            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                PermitExemptionResources.ErrorMessage))
            {
                return View(viewModel);
            }

            await _siteService.SaveSiteAddress(viewModel);
            return RedirectToAction("PermitExemption", "Accreditation", new { viewModel.Id });
        }

        /// <summary>
        /// Stubbed method for the back action from the legal documents address to work
        /// </summary>
        /// <param name="id">accreditation id</param>
        /// <returns>async IActionResult representing the legal documents view</returns>
        [HttpGet("LegalDocuments", Name = "LegalDocuments")]
        public async Task<IActionResult> LegalDocuments(Guid id)
        {
            return NotFound();
        }

        /// <summary>
        /// End point for getting the view for adding the address for legal documentation
        /// </summary>
        /// <param name="id">The accreditation id</param>
        /// <returns>async IActionResult representing the legal document address view</returns>
        [HttpGet("LegalDocumentsAddress", Name = "LegalDocumentsAddress")]
        public async Task<IActionResult> LegalDocumentsAddress(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PopulateBackModel(LegalDocumentsRouteName);

            var viewModel = await _accreditationService.GetLegalDocumentsAddressViewModel(id.Value);

            return View(viewModel);
        }

        /// <summary>
        /// End point to update the address for legal documents
        /// </summary>
        /// <param name="viewModel">The view model as sent from the view</param>
        /// <param name="saveButton">Which save button has been used</param>
        /// <returns>Redirection to the new route action</returns>
        [HttpPost("LegalDocumentsAddress")]
        public async Task<IActionResult> LegalDocumentsAddress(
            LegalDocumentsAddressViewModel viewModel,
            SaveButton saveButton)
        {
            PopulateBackModel(LegalDocumentsRouteName);

            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                AddressResources.AddressLine1Missing,
                AddressResources.TownOrCityMisssing,
                AddressResources.PostCodeMissing))
            {
                return View(viewModel);
            }

            await _accreditationService.UpdateLegalDocumentsAddress(viewModel);
            return RedirectToRoute(
                RegulatorContactRouteName,
                new
                {
                    id = viewModel.Id
                });
        }

        /// <summary>
        /// Stubbed method for "Who can the regulator contact about this application
        /// </summary>
        /// <param name="id">Accreditation id</param>
        /// <returns>the relevant view or response</returns>
        [HttpGet("RegulatorContact", Name = "RegulatorContact")]
        public async Task<IActionResult> RegulatorContact(Guid id)
        {
            return NotFound();
        }

        /// <summary>
        /// Returns PRN tonnage data view.
        /// </summary>
        /// <param name="id">Accreditation id.</param>
        /// <returns>PRN tonnage data view.</returns>
        [HttpGet("PrnTonnesPlanned")]
        public async Task<IActionResult> PrnTonnesPlanned(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            PrnTonnesPlannedViewModel vm = await _accreditationService.GetPrnTonnesPlanned(id.Value);
            return View(vm);
        }

        /// <summary>
        /// Updates PRN tonnage data.
        /// </summary>
        /// <param name="id">Accreditation id.</param>
        /// <param name="vm">View model for PRN tonnage data.</param>
        /// <returns>Redirects to Declaration page.</returns>
        [HttpPost("PrnTonnesPlanned")]
        public async Task<IActionResult> PrnTonnesPlanned(
            Guid? id,
            PrnTonnesPlannedViewModel vm)
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

        /// <summary>
        /// Placeholder for Declaration page.
        /// </summary>
        /// <param name="id">Accreditation id.</param>
        /// <returns>Declaration view.</returns>
        [HttpGet("Declaration")]
        [Route("Declaration")]
        public async Task<IActionResult> Declaration(
            Guid? id)
        {
            return NotFound();
        }

        /// <summary>
        /// Completion view model.
        /// </summary>
        /// <param name="id">The accrediationid.</param>
        /// <param name="countryCode">The country code.</param>
        /// <returns>Completion view.</returns>
        [HttpGet("Completion")]
        public async Task<IActionResult> Completion(Guid? id, string countryCode)
        {
            PopulateBackModel(LegalDocumentsRouteName);

            if (!id.HasValue)
            {
                return BadRequest();
            }

            CompletionViewModel vm = await _accreditationService.Completion(id.Value, countryCode);
            return View(vm);
        }
    }
}
