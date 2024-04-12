// <copyright file="AccreditationSiteController.cs" company="DEFRA">
// Copyright (c) DEFRA All rights reserved.
// </copyright>

namespace EPR.Accreditation.Portal.Controllers
{
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Extensions;
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]/{id}/Site/")]
    public class AccreditationSiteController : Controller
    {
        private readonly IAccreditationSiteService _accreditationSiteService;
        private readonly ISaveAndComeBackService _saveAndComeBackService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BackPageViewModel _backPageViewModel;

        public AccreditationSiteController(
            IAccreditationSiteService accreditationSiteService,
            ISaveAndComeBackService saveAndComeBackService,
            IHttpContextAccessor httpContextAccessor,
            BackPageViewModel backPageViewModel
            )
        {
            _accreditationSiteService = accreditationSiteService ?? throw new ArgumentNullException(nameof(accreditationSiteService));
            _saveAndComeBackService = saveAndComeBackService ?? throw new ArgumentNullException(nameof(saveAndComeBackService));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _backPageViewModel = backPageViewModel;
        }

        [HttpGet("ExemptionReferences")]
        public async Task<IActionResult> ExemptionReferences(Guid? id)
        {
            _backPageViewModel.Url = $"/Accreditation/{id}/PermitExemption";

            if (id == null)
                return NotFound();
            ;
            var viewModel = await _accreditationSiteService.GetExemptionReferencesViewModel(id.Value);

            return View(viewModel);
        }

        [HttpPost("ExemptionReferences")]
        public async Task<IActionResult> ExemptionReferences(
            ExemptionReferencesViewModel viewModel,
            SaveButton saveButton)
        {
            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                ExemptionReferencesResources.ErrorMessageBlank,
                ExemptionReferencesResources.ErrorMessageDuplicate,
                ExemptionReferencesResources.ErrorMessageInvalidFormat,
                ExemptionReferencesResources.ErrorMessageTooLong))
            {
                _backPageViewModel.Url = $"/Accreditation/{viewModel.Id}/PermitExemption";
                return View(viewModel);
            }

            await _accreditationSiteService.UpdateExemptionReferences(viewModel);

            if (saveButton == SaveButton.SaveAndContinue)
                return RedirectToAction("HowManyTonnes", "Accreditation");

            // this is all the data we require to save for come back later
            await _saveAndComeBackService.AddSaveAndComeBack(
                viewModel.Id,
                _httpContextAccessor.HttpContext.GetRouteData().Values);
            return View("_ApplicationSaved");
        }
    }
}
