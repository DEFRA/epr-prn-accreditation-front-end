namespace EPR.Accreditation.Portal.Controllers
{
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.Extensions;
    using EPR.Accreditation.Portal.Helpers.Interfaces;
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Base class for Sites and Overseas Sites
    /// </summary>
    public abstract class BaseSiteController : Controller
    {
        // overriden in inheriting classes
        protected string SiteChooseMaterialRouteName;
        protected string SiteProcessingCapacityRouteName;
        protected string SiteProductsProducedRouteName;
        protected string SiteNonWasteInputsRouteName;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IUrlHelperWrapper _urlHelper;
        protected readonly IAccreditationSiteMaterialService _accreditationSiteMaterialService;
        protected readonly ISaveAndComeBackService _saveAndComeBackService;
        protected readonly BackPageViewModel _backPageViewModel;
        private SiteType _siteType;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSiteController"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">httpContextAccessor</param>
        /// <param name="urlHelper">urlHelper</param>
        /// <param name="accreditationSiteMaterialService">Service layer for accreditation site materials</param>
        /// <param name="saveAndComeBackService">service layer for save and come back later</param>
        /// <param name="backPageViewModel">View model for using the back button</param>
        /// <param name="siteType">The site of type this instance is being created for</param>
        /// <exception cref="ArgumentNullException">Throws if any of the parameters have not been intialized</exception>
        protected BaseSiteController(
            IHttpContextAccessor httpContextAccessor,
            IUrlHelperWrapper urlHelper,
            IAccreditationSiteMaterialService accreditationSiteMaterialService,
            ISaveAndComeBackService saveAndComeBackService,
            BackPageViewModel backPageViewModel,
            SiteType siteType)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _siteType = siteType;
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
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
            PopulateBackModel(_siteType == SiteType.OverseasSite ? "OverseasSiteChooseMaterial" : "SiteChooseMaterial");

            var wasteSource = await _accreditationSiteMaterialService.GetWasteSource(
                _siteType,
                id.Value,
                siteId,
                materialId.Value);

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
            await _accreditationSiteMaterialService.UpdateWasteSource(
                _siteType,
                viewModel);

            if (saveButton == SaveButton.SaveAndComeBack)
            {
                PopulateBackModel(SiteChooseMaterialRouteName);

                // this is all the data we require to save for come back later
                await _saveAndComeBackService.AddSaveAndComeBack(
                    viewModel.Id,
                    _httpContextAccessor.HttpContext.GetRouteData().Values);
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

        protected void PopulateBackModel(string action)
        {
            var idValue = _httpContextAccessor.HttpContext.Request.RouteValues["id"];
            var materialIdValue = _httpContextAccessor.HttpContext.Request.RouteValues["materialId"];

            _backPageViewModel.Url = _urlHelper.RouteUrl(
                action,
                new
                {
                    id = idValue,
                    materialId = materialIdValue
                });
        }
    }
}
