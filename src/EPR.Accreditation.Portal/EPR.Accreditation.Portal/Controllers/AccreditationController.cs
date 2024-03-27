using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.Extensions;
using EPR.Accreditation.Portal.Resources;
using EPR.Accreditation.Portal.Services.Accreditation;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    [Route("[controller]/{id}")]
    public class AccreditationController : Controller
    {
        private readonly IAccreditationService _accreditationService;
        protected readonly IWastePermitService _wastePermitService;
        protected readonly ISaveAndComeBackService _saveAndComeBackService;
        protected readonly BackPageViewModel _backPageViewModel;
        protected IUrlHelper _urlHelper;

        public AccreditationController(
            IWastePermitService wastePermitService,
            ISaveAndComeBackService saveAndComeBackService,
            IAccreditationService accreditationService,
            IUrlHelper urlHelper,
            BackPageViewModel backPageViewModel
            )
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _wastePermitService = wastePermitService ?? throw new ArgumentNullException(nameof(wastePermitService));
            _saveAndComeBackService = saveAndComeBackService ?? throw new ArgumentNullException(nameof(saveAndComeBackService));
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
            _backPageViewModel = backPageViewModel;
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
    }
}
