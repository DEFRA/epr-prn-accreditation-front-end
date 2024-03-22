using EPR.Accreditation.Portal.Constants;
using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Localization;

namespace EPR.Accreditation.Portal.Services.Accreditation
{
    public class AccreditationSiteMaterialService : IAccreditationSiteMaterialService
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IHttpSiteMaterialService _httpSiteMaterialService;

        public AccreditationSiteMaterialService(
            IHttpContextAccessor httpContextAccessor,
            IHttpSiteMaterialService httpSiteMaterialService)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _httpSiteMaterialService = httpSiteMaterialService ?? throw new ArgumentNullException(nameof(httpSiteMaterialService));
        }

        /// <summary>
        /// Gets the name of the waste meterial for this
        /// accreditation, site and material
        /// </summary>
        public async Task<string> GetWasteName(
            Guid id, 
            Guid siteId, 
            Guid materialId)
        {
            // identify the language
            var requestCultureFeature = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            var currentCulture = requestCultureFeature.RequestCulture.Culture;
            var language = Enums.Language.Undefined;

            if (currentCulture.Name == CultureConstants.English.Name)
                language = Enums.Language.English;
            else if (currentCulture.Name == CultureConstants.Welsh.Name)
                language = Enums.Language.Welsh;

            return await _httpSiteMaterialService.GetMeterialName(id, siteId, materialId, language);
        }

        public async Task<WasteSourceViewModel> GetWasteSource(
            Guid id, 
            Guid siteId, 
            Guid materialId)
        {
            return new WasteSourceViewModel
            {
                WasteSource = await _httpSiteMaterialService.GetWasteSource(id, siteId, materialId)
            };
        }

        public async Task UpdateWasteSource(WasteSourceViewModel wasteSourceViewModel)
        {
            await _httpSiteMaterialService.UpdateWasteSource(
                wasteSourceViewModel.Id,
                wasteSourceViewModel.SiteId,
                wasteSourceViewModel.MaterialId, 
                wasteSourceViewModel.WasteSource);
        }
    }
}
