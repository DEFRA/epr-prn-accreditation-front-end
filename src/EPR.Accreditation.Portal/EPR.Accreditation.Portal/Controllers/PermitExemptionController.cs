using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.Services.PermitExemption.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    public class PermitExemptionController : Controller
    {
        protected readonly IPermitExemptionService _permitExemptionService;

        public PermitExemptionController(IPermitExemptionService permitExemptionService)
        {
            _permitExemptionService = permitExemptionService ?? throw new ArgumentNullException(nameof(permitExemptionService));
        }

        [HttpGet]
        public async Task<IActionResult> CheckWastePermitExemption(Guid? id)
        {
            if (id == null)
                return NotFound();

            var viewModel = await _permitExemptionService.GetPermitExemptionViewModel(id.Value);

            return View(viewModel);
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
