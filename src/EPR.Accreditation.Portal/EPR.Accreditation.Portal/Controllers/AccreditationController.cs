using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
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

        [HttpGet, ActionName("WasteLicensesAndPermits")]
        public async Task<IActionResult> WasteLicensesAndPermits(Guid? id)
        {
            if (id == null)
                return NotFound();

            var viewModel = _accreditationService.GetWastePermitViewModel(id.Value);

            return View(viewModel);
        }

        [HttpPost, ActionName("WasteLicensesAndPermits")]
        public async Task<IActionResult> WasteLicensesAndPermits(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel)
        {
            if (!ModelState.IsValid)
                return View(wasteLicensesAndPermitsViewModel);

            await _accreditationService.SaveWastePermit(wasteLicensesAndPermitsViewModel);
            
            return View("CheckWastePermitExemption");
        }
    }
}
