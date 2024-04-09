using AutoMapper;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Portal.Common.Dtos.Portal;
using EPR.Accreditation.Portal.Constants;
using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Localization;

namespace EPR.Accreditation.Portal.Services.Accreditation
{
    public class AccreditationSiteMaterialService : IAccreditationSiteMaterialService
    {
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IHttpSiteMaterialService _httpSiteMaterialService;

        public AccreditationSiteMaterialService(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IHttpSiteMaterialService httpSiteMaterialService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _httpSiteMaterialService = httpSiteMaterialService ?? throw new ArgumentNullException(nameof(httpSiteMaterialService));
        }

        /// <summary>
        /// Gets the name of the waste meterial for this
        /// accreditation, site and material
        /// </summary>
        public async Task<string> GetWasteName(
            Guid id,
            Guid? siteId,
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

            return await _httpSiteMaterialService.GetMeterialName(
                id, 
                siteId, 
                materialId, 
                language);
        }

        public async Task<WasteSourceViewModel> GetWasteSource(
            SiteType siteType,
            Guid id,
            Guid? siteId,
            Guid materialId)
        {
            return new WasteSourceViewModel
            {
                WasteSource = await _httpSiteMaterialService.GetWasteSource(
                    siteType,
                    id,
                    siteId,
                    materialId)
            };
        }

        public async Task UpdateWasteSource(
            SiteType siteType,
            WasteSourceViewModel wasteSourceViewModel)
        {
            // this field is a required field. Therefore, if it's got to this point
            // then "Save and come back later" has been selected and we are letting
            // blank required fields through
            if (wasteSourceViewModel.WasteSource == null)
                wasteSourceViewModel.WasteSource = string.Empty;

            await _httpSiteMaterialService.UpdateWasteSource(
                siteType,
                wasteSourceViewModel.Id,
                wasteSourceViewModel.SiteId,
                wasteSourceViewModel.MaterialId,
                wasteSourceViewModel.WasteSource);
        }

        // =====================================================

        public async Task<MaterialOutputsViewModel> GetMaterialOutputs(
            Guid id,
            Guid materialId)
        {
            var materialOutputsDto = await _httpSiteMaterialService.GetMaterialOutputs(
                id,
                materialId);

            return _mapper.Map<MaterialOutputsViewModel>(materialOutputsDto);
        }

        public async Task UpdateMaterialOutputs(
            MaterialOutputsViewModel materialOutputsViewModel)
        {
            var materialOutputsDto = _mapper.Map<MaterialOutputsDto>(materialOutputsViewModel);

            await _httpSiteMaterialService.UpdateMaterialOutputs(
                materialOutputsViewModel.Id,
                materialOutputsViewModel.MaterialId,
                materialOutputsDto);
        }


        public async Task<MaterialWasteOutputsViewModel> GetMaterialWasteOutputs(
            Guid id,
            Guid materialId)
        {
            var materialWasteOutputsDto = await this._httpSiteMaterialService.GetMaterialWasteOutputs(
                id,
                materialId);

            return this._mapper.Map<MaterialWasteOutputsViewModel>(materialWasteOutputsDto);
        }

        public async Task UpdateMaterialWasteOutputs(MaterialWasteOutputsViewModel materialOutputsViewModel)
        {
            var materialWasteOutputsDto = _mapper.Map<MaterialWasteOutputsDto>(materialOutputsViewModel);

            await _httpSiteMaterialService.UpdateMaterialWasteOutputs(
                materialOutputsViewModel.Id,
                materialOutputsViewModel.MaterialId,
                materialWasteOutputsDto);
        }

        // =====================================================


        public async Task<ReprocessedWasteLastYearViewModel> GetReprocessedWasteLastYearViewModel(
            Guid id,
            Guid materialId)
        {
            return new ReprocessedWasteLastYearViewModel
            {
                Id = id,
                HasReprocessedWasteLastYear = await _httpSiteMaterialService.GetReprocessedWasteLastYear(
                    id,
                    materialId)
            };
        }

        public async Task UpdateReprocessedWasteLastYear(
            ReprocessedWasteLastYearViewModel reprocessedWasteLastYearViewModel)
        {
            var reprocessedWasteLastYearDto = _mapper.Map<DTOs.MaterialReprocessorDetails.ReprocessedWasteLastYear>(
                reprocessedWasteLastYearViewModel);

            await _httpSiteMaterialService.UpdateReprocessedWasteLastYear(
                reprocessedWasteLastYearViewModel.Id,
                reprocessedWasteLastYearViewModel.MaterialId,
                reprocessedWasteLastYearDto);
        }
    }
}
