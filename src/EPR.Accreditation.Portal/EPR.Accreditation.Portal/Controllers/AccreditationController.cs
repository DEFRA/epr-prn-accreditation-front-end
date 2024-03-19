using EPR.Accreditation.App.Enums;
using EPR.Accreditation.App.Services.Interfaces;
using EPR.Accreditation.App.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    public class AccreditationController : Controller
    {
        private readonly IAccreditationService _accreditationService;

        public AccreditationController(IAccreditationService accreditationService)
        {
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
        }

        [HttpGet]
        public IActionResult ApplicationSaved(Guid? id)
        {
            if (id == null)
                return NotFound();

            var viewModel = _accreditationService.GetApplicationSavedViewModel(id.Value);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CheckWastePermitExemption(Guid? id)
        {
            if (id == null)
                return NotFound();

            //var viewModel = _accreditationService.GetPermitExemptionViewModel(id.Value);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckWastePermitExemption(
            PermitExemptionViewModel viewModel,
            SaveButtonValues saveAndContinue)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            //await _accreditationService.SaveWastePermitExemption(viewModel);

            if (saveAndContinue == SaveButtonValues.SaveAndContinue)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("ApplicationSaved", "Accreditation", new { viewModel.Id });
            }
        }
    }
}