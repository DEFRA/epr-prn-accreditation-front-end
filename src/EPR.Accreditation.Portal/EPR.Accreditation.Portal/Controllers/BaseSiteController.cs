using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Portal.Controllers
{
    public abstract class BaseSiteController : Controller
    {
        protected readonly SiteType _siteType;
        protected readonly IAccreditationSiteMaterialService _accreditationSiteMaterialService;

        protected BaseSiteController(
            IAccreditationSiteMaterialService accreditationSiteMaterialService,
            SiteType siteType)
        {
            if (siteType == SiteType.None)
                throw new ArgumentException("Site enum cannot be 'None'");

            _siteType = siteType;
            _accreditationSiteMaterialService = accreditationSiteMaterialService ?? throw new ArgumentNullException(nameof(accreditationSiteMaterialService));
        }

        protected async Task<IActionResult> GetMaterialWasteSource(
            Guid? id,
            Guid? siteId,
            Guid? materialId)
        {
            var wasteSource = await _accreditationSiteMaterialService.GetWasteSource(id.Value, siteId.Value, materialId.Value);

            return View(wasteSource);
        }

        
        protected async Task<IActionResult> SaveMaterialWasteSource(WasteSourceViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return await GetMaterialWasteSource(
                    viewModel.Id,
                    viewModel.SiteId,
                    viewModel.MaterialId);
            }

            await _accreditationSiteMaterialService.UpdateWasteSource(viewModel);

            var routeName = string.Empty;

            if(_siteType == SiteType.Site)
            {
                routeName = "SiteProcessingCapacity";
            }
            else
            {
                routeName = "OverseasSiteProcessingCapacity";
            }

            return RedirectToRoute(routeName,
                new
                {
                    viewModel.Id,
                    viewModel.SiteId,
                    viewModel.MaterialId
                });
        }
    }
}
