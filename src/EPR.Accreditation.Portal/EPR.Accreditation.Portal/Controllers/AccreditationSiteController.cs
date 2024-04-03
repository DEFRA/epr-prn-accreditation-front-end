using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    [Route("[controller]/{id}/Site/{siteId}")]
    public class AccreditationSiteController : Controller
    {
        protected readonly IAccreditationSiteService _accreditationSiteService;
        protected readonly ISaveAndComeBackService _saveAndComeBackService;
        protected readonly BackPageViewModel _backPageViewModel;
        protected IUrlHelper _urlHelper;


        public AccreditationSiteController(
            IAccreditationSiteService accreditationSiteService,
            ISaveAndComeBackService saveAndComeBackService,
            BackPageViewModel backPageViewModel,
            IUrlHelper urlHelper
            )
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _saveAndComeBackService = saveAndComeBackService ?? throw new ArgumentNullException(nameof(saveAndComeBackService));
            _backPageViewModel = backPageViewModel;
            _accreditationSiteService = accreditationSiteService ?? throw new ArgumentNullException(nameof(accreditationSiteService));
        }

        [HttpGet("ExemptionReferences")]
        public async Task<IActionResult> ExemptionReferences(
            Guid? id,
            Guid? siteId)
        {
            // TODO: Need to add correct back link in the future
            _backPageViewModel.Url = _urlHelper.ActionLink("ApplyForAccreditation", "Home");
            if (id == null)
                return NotFound();

            //var viewModel = await _accreditationSiteService.GetExemptionReferencesViewModel(
            //    id.Value,
            //    siteId.Value);

            return View();
        }

        [HttpPost("ExemptionReferences")]
        public async Task<IActionResult> ExemptionReferences(
            Guid? id,
            Guid? siteId,
            ExemptionReferencesViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // If the ModelState is not valid, return the view with validation errors
                return View(viewModel);
            }

            // At this point, the model state is valid, and you can access the reference numbers
            // from viewModel.ReferenceNumber1, viewModel.ReferenceNumber2, etc.
            // You can also access all reference numbers from viewModel.ExemptionReferences.

            // Here, you can process the data, save it to a database, etc.

            return RedirectToAction("Success");
        }
    }
}
