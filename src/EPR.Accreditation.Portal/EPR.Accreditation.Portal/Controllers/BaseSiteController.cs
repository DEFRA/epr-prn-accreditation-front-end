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
    public abstract class BaseSiteController : BaseController
    {
        // overriden in inheriting classes
#pragma warning disable SA1401 // FieldsMustBePrivate
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IAccreditationSiteMaterialService _accreditationSiteMaterialService;
        protected string _siteChooseMaterialRouteName;
        protected string _siteProcessingCapacityRouteName;
        protected string _siteNonWasteInputsRouteName;
#pragma warning restore SA1401 // FieldsMustBePrivate
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
            BackPageViewModel backPageViewModel,
            SiteType siteType)
            : base(
                  httpContextAccessor,
                  urlHelper,
                  backPageViewModel)
        {
            _siteType = siteType;
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _accreditationSiteMaterialService = accreditationSiteMaterialService ?? throw new ArgumentNullException(nameof(accreditationSiteMaterialService));
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
            PopulateBackModel(_siteChooseMaterialRouteName);

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

            return RedirectToRoute(
                _siteProcessingCapacityRouteName,
                new
                {
                    viewModel.Id,
                    viewModel.SiteId,
                    viewModel.MaterialId
                });
        }
    }
}
