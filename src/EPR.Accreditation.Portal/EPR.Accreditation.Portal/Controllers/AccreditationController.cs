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

        [HttpGet]
        [ActionName("OperatorType")]
        public async Task<IActionResult> OperatorType(Guid? id)
        {
            if (id.HasValue)
            {
                var operatorType = await _accreditationService.GetOperatorType(id.Value);

                return View(operatorType);

            }

            return View(new OperatorTypeViewModel());
        }

        [HttpPost]
        [ActionName("OperatorType")]
        public async Task<IActionResult> OperatorType(OperatorTypeViewModel vm)
        {
            if(!ModelState.IsValid)
                return View(vm);

            var externalId = await _accreditationService.CreateAccreditation(vm);

            return RedirectToAction("Index", "Home");
        }
    }
}
