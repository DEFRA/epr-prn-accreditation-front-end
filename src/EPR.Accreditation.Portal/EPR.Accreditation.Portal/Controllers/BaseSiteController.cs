using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.Extensions;
using EPR.Accreditation.Portal.Resources;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    public abstract class BaseSiteController : Controller
    {
        // overriden in inheriting classes
        protected string SiteProcessingCapacityRouteName;
        protected readonly SiteType _siteType;
        protected IUrlHelper _urlHelper;
        protected readonly IAccreditationSiteMaterialService _accreditationSiteMaterialService;
        protected readonly ISaveAndComeBackService _saveAndComeBackService;
        protected readonly BackPageViewModel _backPageViewModel;

        protected BaseSiteController(
            IUrlHelper urlHelper,
            IAccreditationSiteMaterialService accreditationSiteMaterialService,
            ISaveAndComeBackService saveAndComeBackService,
            BackPageViewModel backPageViewModel,
            SiteType siteType)
        {
            if (siteType == SiteType.None)
                throw new ArgumentException("Site enum cannot be 'None'");

            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _siteType = siteType;
            _accreditationSiteMaterialService = accreditationSiteMaterialService ?? throw new ArgumentNullException(nameof(accreditationSiteMaterialService));
            _saveAndComeBackService = saveAndComeBackService ?? throw new ArgumentNullException(nameof(saveAndComeBackService));
            _backPageViewModel = backPageViewModel ?? throw new ArgumentNullException(nameof(backPageViewModel));
        }

        protected async Task<IActionResult> GetMaterialWasteSource(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            // need to add back link
            _backPageViewModel.Url = _urlHelper.ActionLink(
                "ChooseMaterial",
                _siteType == SiteType.OverseasSite ? "OverseasSite" : "Site",
                new object[] { id, siteId, materialId });

            var obj = new 
            {
                id = id,
                siteId = siteId,
                materialId = materialId
            };

            _backPageViewModel.Url = _urlHelper.RouteUrl(
                _siteType == SiteType.OverseasSite ? "OverseasSiteChooseMaterial" : "SiteChooseMaterial",
                obj);

            var wasteSource = await _accreditationSiteMaterialService.GetWasteSource(id.Value, siteId.Value, materialId.Value);

            return View(wasteSource);
        }

        protected async Task<IActionResult> SaveMaterialWasteSource(
            WasteSourceViewModel viewModel,
            SaveButton saveButton)
        {
            if (!ModelState.IsValidForSaveForLater(
                saveButton,
                WasteSourceResources.NoSourceSupplied))
            {
                return await GetMaterialWasteSource(
                    viewModel.Id,
                    viewModel.SiteId,
                    viewModel.MaterialId);
            }

            // save the data regardless of whether this is save and continue or
            // save and come back later
            await _accreditationSiteMaterialService.UpdateWasteSource(viewModel);

            if (saveButton == SaveButton.SaveAndComeBack)
            {
                // this is all the data we require to save for come back later
                await _saveAndComeBackService.AddSaveAndComeBack(
                    viewModel.Id,
                    Request.HttpContext.GetRouteData().Values);
                return View("_ApplicationSaved");
            }
            else
            {
                return RedirectToRoute(SiteProcessingCapacityRouteName,
                    new
                    {
                        viewModel.Id,
                        viewModel.SiteId,
                        viewModel.MaterialId
                    });
            }
        }
    }
}
