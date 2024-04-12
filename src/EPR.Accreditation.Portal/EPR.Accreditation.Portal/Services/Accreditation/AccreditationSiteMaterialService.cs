using AutoMapper;
using EPR.Accreditation.Portal.Common.Dtos.Portal;
using EPR.Accreditation.Portal.Constants;
using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.RESTservices.Interfaces;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using Microsoft.AspNetCore.Localization;
using static EPR.Accreditation.Portal.Constants.Strings;

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
                    materialId),
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

        /// <summary>
        /// Gets the non waste inputs and performs any necessary manipulation before
        /// returning it to the controller
        /// </summary>
        /// <param name="id">Accreditation Id</param>
        /// <param name="materialId">Material Id</param>
        /// <returns>The view model for non waste inputs</returns>
        public async Task<NonWasteInputsViewModel> GetNonWasteInputs(Guid id, Guid materialId)
        {
            var nonWasteInputsDto = await _httpSiteMaterialService.GetNonWasteInputs(
                id,
                materialId);

            var viewModel = _mapper.Map<NonWasteInputsViewModel>(nonWasteInputsDto);

            if (viewModel != null &&
                viewModel.Rows?.Count <= GenericConstants.MinimumMultiLineRecordNumber)
            {
                for (var i = viewModel.Rows.Count; i < GenericConstants.MinimumMultiLineRecordNumber; i++)
                {
                    viewModel.Rows.Add(new NonWasteInputsRowViewModel());
                }
            }

            return viewModel;
        }

        /// <summary>
        /// Performs any necessary logic on the non waste inputs, then requests
        /// the data is sent to be saved
        /// </summary>
        /// <param name="nonWasteInputsViewModel">View model from the view to be converted into a DTO</param>
        /// <returns>Nothing (async Task)</returns>
        public async Task UpdateNonWasteInputs(NonWasteInputsViewModel nonWasteInputsViewModel)
        {
            if (nonWasteInputsViewModel.Rows != null &&
                nonWasteInputsViewModel.Rows.Any())
            {
                // remove blank rows
                nonWasteInputsViewModel.Rows = nonWasteInputsViewModel
                    .Rows
                    .Where(r => !string.IsNullOrWhiteSpace(r.Type) && r.Tonnes != null)
                    .ToList();
            }

            var nonWasteInputsDto = _mapper.Map<NonWasteInputsDto>(nonWasteInputsViewModel);

            await _httpSiteMaterialService.UpdateNonWasteInputs(
                nonWasteInputsDto.Id,
                nonWasteInputsDto.MaterialId,
                nonWasteInputsDto);
        }

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
