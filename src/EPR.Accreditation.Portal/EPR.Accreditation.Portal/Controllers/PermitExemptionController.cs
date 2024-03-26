using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.Extensions;
using EPR.Accreditation.Portal.Resources;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.Services.PermitExemption.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    public class PermitExemptionController : Controller
    {
        protected readonly IPermitExemptionService _permitExemptionService;
        protected readonly ISaveAndComeBackService _saveAndComeBackService;
        protected readonly BackPageViewModel _backPageViewModel;
        protected IUrlHelper _urlHelper;

        public PermitExemptionController(
            IPermitExemptionService permitExemptionService,
            ISaveAndComeBackService saveAndComeBackService,
            IUrlHelper urlHelper,
            BackPageViewModel backPageViewModel)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _permitExemptionService = permitExemptionService ?? throw new ArgumentNullException(nameof(permitExemptionService));
            _saveAndComeBackService = saveAndComeBackService ?? throw new ArgumentNullException(nameof(saveAndComeBackService));
            _backPageViewModel = backPageViewModel;
        }

        [HttpGet]
        public async Task<IActionResult> CheckWastePermitExemption(Guid? id)
        {
            // need to add back link
            _backPageViewModel.Url = _urlHelper.ActionLink("Index", "Home"); //TODO: Need to change with Sajid's endpoint

            if (id == null)
                return NotFound();

            var viewModel = await _permitExemptionService.GetPermitExemptionViewModel(id.Value);

            return View(viewModel);
        }

        [HttpPost]
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

            await _permitExemptionService.UpdatePermitExemption(viewModel);

            if (saveButton == SaveButton.SaveAndContinue)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // this is all the data we require to save for come back later
                await _saveAndComeBackService.AddSaveAndComeBack(
                    viewModel.Id,
                    Request.HttpContext.GetRouteData().Values);
                return View("_ApplicationSaved");
            }
        }
    }
}
