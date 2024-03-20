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
        public async Task<IActionResult> WasteLicensesAndPermits(int? id)
        {
            return View(
                "WasteLicensesAndPermits");
        }

        [HttpPost]
        [ActionName("WasteLicensesAndPermits")]
        public async Task<IActionResult> WasteLicensesAndPermits(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel)
        {
            //if (!ModelState.IsValid)
            //    return View(wasteLicensesAndPermitsViewModel);

            //var accreditation = _httpAccreditationServicel.GetAccreditation("6E04132D-9E52-4853-BE1E-D48C2DCA82BA");
            //_httpAccreditationServicel.CreateWastePermit(1, 1);

            return View(
                "WasteLicensesAndPermits");
        }
    }
}