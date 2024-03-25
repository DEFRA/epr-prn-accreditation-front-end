using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    public class PermitExemptionController : Controller
    {
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
