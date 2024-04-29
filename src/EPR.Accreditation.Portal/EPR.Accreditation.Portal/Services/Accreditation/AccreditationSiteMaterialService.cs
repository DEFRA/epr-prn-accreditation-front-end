namespace EPR.Accreditation.Portal.Services.Accreditation
{
    using AutoMapper;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.Constants;
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using EPR.Accreditation.Portal.ViewModels.SiteMaterial;
    using Microsoft.AspNetCore.Localization;
    using static EPR.Accreditation.Portal.Constants.Strings;

    public class AccreditationSiteMaterialService : IAccreditationSiteMaterialService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpSiteMaterialService _httpSiteMaterialService;

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
            SiteType siteType,
            Guid id,
            Guid materialId)
        {
            // identify the language
            var requestCultureFeature = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            var currentCulture = requestCultureFeature.RequestCulture.Culture;
            var language = Enums.Language.Undefined;

            if (currentCulture.Name == CultureConstants.English.Name)
            {
                language = Enums.Language.English;
            }
            else if (currentCulture.Name == CultureConstants.Welsh.Name)
            {
                language = Enums.Language.Welsh;
            }

            return await _httpSiteMaterialService.GetMeterialName(
                siteType,
                id,
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
            {
                wasteSourceViewModel.WasteSource = string.Empty;
            }

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
        public async Task<NonWasteInputsViewModel> GetNonWasteInputs(
            Guid id,
            Guid materialId)
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
                    viewModel.Rows.Add(new TypeTonnesRowViewModel());
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

            var nonWasteInputsDto = _mapper.Map<ReprocessingSupportingInformationDto>(nonWasteInputsViewModel);

            await _httpSiteMaterialService.UpdateNonWasteInputs(
                nonWasteInputsDto.Id,
                nonWasteInputsDto.MaterialId,
                nonWasteInputsDto);
        }

        /// <summary>
        /// Gets the products produced and performs any necessary manipulation before
        /// returning it to the controller
        /// </summary>
        /// <param name="id">Accreditation Id</param>
        /// <param name="materialId">Material Id</param>
        /// <returns>The view model for products produced</returns>
        public async Task<ProductsProducedViewModel> GetProductsProduced(
            Guid id,
            Guid materialId)
        {
            var productsProducedDto = await _httpSiteMaterialService.GetProductsProduced(
                id,
                materialId);

            var viewModel = _mapper.Map<ProductsProducedViewModel>(productsProducedDto);

            if (viewModel != null &&
                viewModel.Rows?.Count <= GenericConstants.MinimumMultiLineRecordNumber)
            {
                for (var i = viewModel.Rows.Count; i < GenericConstants.MinimumMultiLineRecordNumber; i++)
                {
                    viewModel.Rows.Add(new TypeTonnesRowViewModel());
                }
            }

            return viewModel;
        }

        /// <summary>
        /// Performs any necessary logic on the products produced, then requests
        /// the data is sent to be saved
        /// </summary>
        /// <param name="productsProducedViewModel">View model from the view to be converted into a DTO</param>
        /// <returns>Nothing (async Task)</returns>
        public async Task UpdateProductsProduced(ProductsProducedViewModel productsProducedViewModel)
        {
            if (productsProducedViewModel == null)
            {
                throw new NullReferenceException(nameof(productsProducedViewModel));
            }

            if (productsProducedViewModel.Rows != null &&
                productsProducedViewModel.Rows.Any())
            {
                // remove blank rows
                productsProducedViewModel.Rows = productsProducedViewModel
                    .Rows
                    .Where(r => !string.IsNullOrWhiteSpace(r.Type) && r.Tonnes != null)
                    .ToList();
            }

            var nonWasteInputsDto = _mapper.Map<ReprocessingSupportingInformationDto>(productsProducedViewModel);

            await _httpSiteMaterialService.UpdateProductsProduced(
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

        /// <summary>
        /// Gets material waste output.
        /// </summary>
        /// <param name="id">Accreditation id.</param>
        /// <param name="materialId">Material id.</param>
        /// <returns>Material waste output dto.</returns>
        public async Task<MaterialWasteInputsViewModel> GetMaterialWasteInputs(
            Guid id,
            Guid materialId)
        {
            var materialWasteInputsDto = await this._httpSiteMaterialService.GetMaterialWasteInputs(
                id,
                materialId);

            return _mapper.Map<MaterialWasteInputsViewModel>(materialWasteInputsDto);
        }

        /// <summary>
        /// Updates material waste output.
        /// </summary>
        /// <param name="materialInputsViewModel">Material waste output dto.</param>
        /// <returns>Nothing.</returns>
        public async Task UpdateMaterialWasteInputs(MaterialWasteInputsViewModel materialInputsViewModel)
        {
            var materialWasteInputsDto = _mapper.Map<MaterialWasteInputsDto>(materialInputsViewModel);

            await _httpSiteMaterialService.UpdateMaterialWasteInputs(
                materialInputsViewModel.Id,
                materialInputsViewModel.MaterialId,
                materialWasteInputsDto);
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

        /// <summary>
        /// Gets the view model and any relevant data fore the waste description code page
        /// </summary>
        /// <param name="id">The id of the accreditation</param>
        /// <param name="overseasSiteId">The id of the overseas site</param>
        /// <param name="materialId">The id of the material that the waste codes are for</param>
        /// <returns>An async WasteDescriptionCodeViewModel</returns>
        public async Task<WasteDescriptionCodeViewModel> GetWasteDescriptionCodeViewModel(
            Guid id,
            Guid overseasSiteId,
            Guid materialId)
        {
            var wadsteDescriptionCodeList = await _httpSiteMaterialService.GetWasteDescriptionCodes(
                id,
                overseasSiteId,
                materialId);

            return new WasteDescriptionCodeViewModel
            {
                Id = id,
                Rows = _mapper.Map<List<WasteDescriptionCodeRowViewModel>>(wadsteDescriptionCodeList)
            };
        }

        /// <summary>
        /// Updates the waste description codes for the material associated to the accreditation
        /// </summary>
        /// <param name="wasteDescriptionCodeViewModel">The view model posted from the view</param>
        /// <returns>Async task</returns>
        public async Task UpdateWasteDescriptionCodeViewModel(WasteDescriptionCodeViewModel wasteDescriptionCodeViewModel)
        {
            // Remove any blank rows
            var wasteDescriptionCodes = wasteDescriptionCodeViewModel
                .Rows
                .Where(r => r.EntryMade)
                .Select(r => r.WasteDescriptionCode);

            await _httpSiteMaterialService.SaveWasteDescriptionCodes(
                wasteDescriptionCodeViewModel.Id,
                wasteDescriptionCodeViewModel.SiteId,
                wasteDescriptionCodeViewModel.MaterialId,
                wasteDescriptionCodes);
        }

        /// <summary>
        /// Concrete implementation to get the view model that drives the view
        /// </summary>
        /// <param name="id">Accrediation ID</param>
        /// <param name="materialId">Material ID</param>
        /// <returns>Task completed asynchronously</returns>
        public async Task<HasNpwdAccreditationNumViewModel> GetHasAccreditationNumViewModel(
            Guid id,
            Guid materialId)
        {
            return new HasNpwdAccreditationNumViewModel
            {
                Id = id,
                MaterialId = materialId,
                Has2024NPWDAccreditation = await _httpSiteMaterialService.GetHasNpwdAccreditationNumber(id, materialId)
            };
        }

        /// <summary>
        /// Concrete implementation of interface action
        /// </summary>
        /// <param name="viewModel">The relevant view model</param>
        /// <returns>Task completed asynchronously</returns>
        public async Task UpdateHasNpwdAccreditationNumber(HasNpwdAccreditationNumViewModel viewModel)
        {
            var hasNpwdAccreditationNumberDto = _mapper.Map<DTOs.SiteMaterial.NpwdAccreditationNumber>(viewModel);

            await _httpSiteMaterialService.UpdateHasNpwdAccreditationNumber(
                viewModel.Id,
                viewModel.MaterialId,
                hasNpwdAccreditationNumberDto);
        }

        /// <summary>
        /// Concrete implementation to get the view model that drives the view
        /// </summary>
        /// <param name="id">Accrediation ID</param>
        /// <param name="materialId">Material ID</param>
        /// <returns>Task completed asynchronously</returns>
        public async Task<NpwdAccreditationNumViewModel> GetAccreditationNumViewModel(
            Guid id,
            Guid materialId)
        {
            return new NpwdAccreditationNumViewModel
            {
                Id = id,
                MaterialId = materialId,
                NpwdAccreditationNumber = await _httpSiteMaterialService.GetNpwdAccreditationNumber(id, materialId)
            };
        }

        /// <summary>
        /// Concrete implementation of interface action
        /// </summary>
        /// <param name="viewModel">The relevant view model</param>
        /// <returns>Task completed asynchronously</returns>
        public async Task UpdateNpwdAccreditationNumber(NpwdAccreditationNumViewModel viewModel)
        {
            var npwdAccreditationNumberDto = viewModel.NpwdAccreditationNumber;

            await _httpSiteMaterialService.UpdateNpwdAccreditationNumber(
                viewModel.Id,
                viewModel.MaterialId,
                npwdAccreditationNumberDto);
        }
    }
}
