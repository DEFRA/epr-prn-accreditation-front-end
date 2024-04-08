using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.Extensions;
using EPR.Accreditation.Portal.Helpers.Interfaces;
using EPR.Accreditation.Portal.Resources;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    [Route("[controller]/{id}/Site/")]
    public class AccreditationSiteController : Controller
    {
        protected readonly IAccreditationSiteService _accreditationSiteService;
        protected readonly ISaveAndComeBackService _saveAndComeBackService;
        protected readonly BackPageViewModel _backPageViewModel;
        protected IUrlHelperWrapper _urlHelper;


        public AccreditationSiteController(
            IAccreditationSiteService accreditationSiteService,
            ISaveAndComeBackService saveAndComeBackService,
            BackPageViewModel backPageViewModel,
            IUrlHelperWrapper urlHelper
            )
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _saveAndComeBackService = saveAndComeBackService ?? throw new ArgumentNullException(nameof(saveAndComeBackService));
            _backPageViewModel = backPageViewModel;
            _accreditationSiteService = accreditationSiteService ?? throw new ArgumentNullException(nameof(accreditationSiteService));
        }

        [HttpGet("ExemptionReferences")]
        public async Task<IActionResult> ExemptionReferences(Guid? id)
        {
            _backPageViewModel.Url = $"/Accreditation/{id}/PermitExemption";

            if (id == null)
                return NotFound();
            ;
            var viewModel = await _accreditationSiteService.GetExemptionReferencesViewModel(id.Value);

            return View(viewModel);
        }

        [HttpPost("ExemptionReferences")]
        public async Task<IActionResult> ExemptionReferences(
            ExemptionReferencesViewModel viewModel,
            SaveButton saveButton)
        {
            if (!ModelState.IsValidForSaveForLater(
                saveButton,
            ExemptionReferencesResources.ErrorMessageBlank,
            ExemptionReferencesResources.ErrorMessageDuplicate,
            ExemptionReferencesResources.ErrorMessageInvalidFormat,
            ExemptionReferencesResources.ErrorMessageTooLong))
                return View(viewModel);

            await _accreditationSiteService.UpdateExemptionReferences(viewModel);

            if (saveButton == SaveButton.SaveAndContinue)
                return RedirectToAction("HowManyTonnes", "Accreditation");

            // this is all the data we require to save for come back later
            await _saveAndComeBackService.AddSaveAndComeBack(
                viewModel.Id,
                Request.HttpContext.GetRouteData().Values);
            return View("_ApplicationSaved");
        }
    }
}
