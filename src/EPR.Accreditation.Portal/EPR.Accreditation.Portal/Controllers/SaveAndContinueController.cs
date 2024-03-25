using EPR.Accreditation.Portal.Services.SaveAndContinue.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    public class SaveAndContinueController : Controller
    {

        private readonly ISaveAndContinueService _saveAndContinueService;

        public SaveAndContinueController(ISaveAndContinueService saveAndContinueService)
        {
            _saveAndContinueService = saveAndContinueService ?? throw new ArgumentNullException(nameof(saveAndContinueService));
        }

        [HttpGet]
        public IActionResult ApplicationSaved(Guid? id)
        {
            if (id == null)
                return NotFound();

            var viewModel = _saveAndContinueService.GetApplicationSavedViewModel(id.Value);

            return View(viewModel);
        }
    }
}
