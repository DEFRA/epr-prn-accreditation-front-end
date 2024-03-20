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
        [ActionName("WasteLicensesAndPermits")]
        public async Task<IActionResult> WasteLicensesAndPermits(Guid? id)
        {
            if (id == null)
                id = Guid.NewGuid();
                //return NotFound();

            var viewModel = await _accreditationService.GetWasteLicensesAndPermitsViewModel(id.Value);

            return View(viewModel);
        }

        [HttpPost]
        [ActionName("WasteLicensesAndPermits")]
        public async Task<IActionResult> WasteLicensesAndPermits(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel)
        {
            if (!ModelState.IsValid)
                return View(wasteLicensesAndPermitsViewModel);

            await _accreditationService.SaveWasteLicensesAndPermitsViewMode(wasteLicensesAndPermitsViewModel);

            return View(wasteLicensesAndPermitsViewModel);
        }
    }
}