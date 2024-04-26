namespace EPR.Accreditation.Portal.Controllers
{
    using EPR.Accreditation.Portal.Constants;
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Extensions;
    using EPR.Accreditation.Portal.Helpers.Interfaces;
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using EPR.Accreditation.Portal.ViewModels.Accreditation;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    [Route("[controller]/{id}")]
    public class AccreditationController : BaseController
    {
        private const string TaskListRouteName = "TaskList";
        private const string CreateOverseasSiteRouteName = "CreateOverseasSite";
        private readonly IAccreditationService _accreditationService;
        private readonly IWastePermitService _wastePermitService;
        private readonly ISiteService _siteService;

        public AccreditationController(
            IHttpContextAccessor httpContextAccessor,
            IWastePermitService wastePermitService,
            IAccreditationService accreditationService,
            IUrlHelperWrapper urlHelper,
            BackPageViewModel backPageViewModel,
            ISiteService siteSerivce)
            : base(
                  httpContextAccessor,
                  urlHelper,
                  backPageViewModel)
        {
            _wastePermitService = wastePermitService ?? throw new ArgumentNullException(nameof(wastePermitService));
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
            _siteService = siteSerivce ?? throw new ArgumentNullException(nameof(siteSerivce));
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

            return actionResult;
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

        [HttpGet("Material/{materialId}/TaskList", Name = "TaskList")]
        public async Task<IActionResult> TaskList(
            Guid? id,
            Guid? materialId)
        {
            if (id != null &&
                materialId != null)
            {
                TaskListViewModel model = await _accreditationService.GetTaskList(
                    id.Value,
                    materialId.Value);
                return View(model);
            }

            return NotFound();
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
                    materialId.Value);
                return View(model);
            }

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

        [HttpGet("SiteAddressView", Name = "SiteAddressView")]
        public async Task<IActionResult> SiteAddress(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _siteService.GetSiteAddressViewModel(id.Value);

            return View(viewModel);
        }

        [HttpPost("SiteAddressView", Name = "SiteAddressView")]
        public async Task<IActionResult> SiteAddress(SiteAddressViewModel viewModel, SaveButton saveButton)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                PermitExemptionResources.ErrorMessage))
            {
                return View(viewModel);
            }

            await _siteService.SaveSiteAddress(viewModel);
            return RedirectToAction("PermitExemption", "Accreditation", new { viewModel.Id });
        }
    }
}
