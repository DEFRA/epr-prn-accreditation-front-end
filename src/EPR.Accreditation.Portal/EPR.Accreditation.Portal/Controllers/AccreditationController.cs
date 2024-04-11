using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.Extensions;
using EPR.Accreditation.Portal.Helpers.Interfaces;
using EPR.Accreditation.Portal.Resources;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    [Route("[controller]/{id}")]
    public class AccreditationController : Controller
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccreditationService _accreditationService;
        protected readonly IWastePermitService _wastePermitService;
        protected readonly ISaveAndComeBackService _saveAndComeBackService;
        protected readonly BackPageViewModel _backPageViewModel;
        protected IUrlHelperWrapper _urlHelper;
        private readonly ISiteService _siteService;

        public AccreditationController(
            IHttpContextAccessor httpContextAccessor,
            IWastePermitService wastePermitService,
            ISaveAndComeBackService saveAndComeBackService,
            IAccreditationService accreditationService,
            IUrlHelperWrapper urlHelper,
            BackPageViewModel backPageViewModel,
            ISiteService siteSerivce)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _wastePermitService = wastePermitService ?? throw new ArgumentNullException(nameof(wastePermitService));
            _saveAndComeBackService = saveAndComeBackService ?? throw new ArgumentNullException(nameof(saveAndComeBackService));
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
            _backPageViewModel = backPageViewModel;
            _siteService = siteSerivce ?? throw new ArgumentNullException(nameof(siteSerivce));
        }

        [HttpGet("PermitExemption")]
        public async Task<IActionResult> CheckWastePermitExemption(Guid? id)
        {
            // TODO: Need to add correct back link in the future
            _backPageViewModel.Url = _urlHelper.ActionLink("ApplyForAccreditation", "Home");

            if (id == null)
                return NotFound();

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
                return View(viewModel);

            await _wastePermitService.UpdatePermitExemption(viewModel);

            if (saveButton == SaveButton.SaveAndContinue && viewModel.HasPermitExemption.Value == true)
                return RedirectToAction("ExemptionReferences", "Accreditation");

            else if (saveButton == SaveButton.SaveAndContinue && viewModel.HasPermitExemption.Value == false)
                return RedirectToAction("AuthorityToIssues", "Accreditation");

            // this is all the data we require to save for come back later
            await _saveAndComeBackService.AddSaveAndComeBack(
                viewModel.Id,
                _httpContextAccessor.HttpContext.GetRouteData().Values);
            return View("_ApplicationSaved");
        }

        [HttpGet("WasteLicensesAndPermits")]
        public async Task<IActionResult> WasteLicensesAndPermits(Guid? id)
        {
            // TODO: Need to add correct back link in the future
            _backPageViewModel.Url = _urlHelper.ActionLink("ApplyForAccreditation", "Home");

            if (id == null)
                return NotFound();

            var viewModel = await _accreditationService.GetWastePermitViewModel(id.Value);

            return View(viewModel);
        }

        [HttpPost("WasteLicensesAndPermits")]
        public async Task<IActionResult> WasteLicensesAndPermits(WasteLicensesAndPermitsViewModel viewModel, SaveButton saveButton)
        {
            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                PermitExemptionResources.ErrorMessage))
                return View(viewModel);

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
                return RedirectToAction("PermitExemption", "Accreditation",
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
                return View(vm);

            var externalId = await _accreditationService.CreateAccreditation(vm);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Site/{siteId}/Material/{materialId}/TaskList", Name = "TaskList")]
        public async Task<IActionResult> TaskList(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            if (id != null && siteId != null && materialId != null)
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

        [HttpGet("Site/{siteId}/Material/{materialId}/SiteAddress", Name = "SiteAddress")]
        public async Task<IActionResult> SiteAddress(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            // TODO: Need to add correct back link in the future
            _backPageViewModel.Url = _urlHelper.ActionLink("ApplyForAccreditation", "Home");

            if (id == null)
                return NotFound();

            var viewModel = await _siteService.GetSiteAddressViewModel(id.Value, siteId.Value, materialId.Value);

            return View(viewModel);
        }

        [HttpPost("Site/{siteId}/Material/{materialId}/SiteAddress", Name = "SiteAddress")]
        public async Task<IActionResult> SiteAddress(SiteAddressViewModel viewModel, SaveButton saveButton)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                PermitExemptionResources.ErrorMessage))
                return View(viewModel);

            await _siteService.SaveSiteAddress(viewModel);

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
                return RedirectToAction("PermitExemption", "Accreditation",
                    new
                    {
                        viewModel.Id
                    });


                return RedirectToAction("Index", "Home");
            }
        }
    }
}
