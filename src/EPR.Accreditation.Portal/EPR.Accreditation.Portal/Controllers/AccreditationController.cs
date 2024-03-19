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
            PermitExemptionViewModel permitExemptionViewModel,
            SaveButtonValues saveAndContinue)
        {
            return View();

        }
    }
}