using EPR.Accreditation.App.Services.Interfaces;
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
        public async Task<IActionResult> ApplicationSaved(int? id)
        {
            if (id == null)
                return NotFound();

            var viewModel = await _accreditationService.GetApplicationSavedViewModel(id.Value);

            return View(viewModel);
        }
    }
}