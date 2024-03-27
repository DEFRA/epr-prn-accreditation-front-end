using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.Extensions;
using EPR.Accreditation.Portal.Resources;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    [Route("[controller]/{id}")]
    public class AccreditationController : Controller
    {
        protected readonly IWastePermitService _wastePermitService;
        protected readonly ISaveAndComeBackService _saveAndComeBackService;
        protected readonly BackPageViewModel _backPageViewModel;
        protected IUrlHelper _urlHelper;
        private readonly IAccreditationService _accreditationService;

        public AccreditationController(
            IWastePermitService wastePermitService,
            ISaveAndComeBackService saveAndComeBackService,
            IUrlHelper urlHelper,
            BackPageViewModel backPageViewModel,
            IAccreditationService accreditationService)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _wastePermitService = wastePermitService ?? throw new ArgumentNullException(nameof(wastePermitService));
            _saveAndComeBackService = saveAndComeBackService ?? throw new ArgumentNullException(nameof(saveAndComeBackService));
            _backPageViewModel = backPageViewModel;
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
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
                Request.HttpContext.GetRouteData().Values);
            return View("_ApplicationSaved");
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
    }
}
