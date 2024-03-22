using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation
{
    public class AccreditationSiteMaterialService : IAccreditationSiteMaterialService
    {
        protected readonly IHttpSiteMaterialService _httpSiteMaterialService;

        public AccreditationSiteMaterialService(IHttpSiteMaterialService httpSiteMaterialService)
        {
            _httpSiteMaterialService = httpSiteMaterialService ?? throw new ArgumentNullException(nameof(httpSiteMaterialService));
        }

        /// <summary>
        /// Gets the name of the waste meterial for this
        /// accreditation, site and material
        /// </summary>
        public async Task<string> GetWasteName(Guid id, Guid siteId, Guid materialId)
        {
            return await _httpSiteMaterialService.GetMeterialName(id, siteId, materialId);
        }

        public async Task<WasteSourceViewModel> GetWasteSource(Guid id, Guid siteId, Guid materialId)
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
