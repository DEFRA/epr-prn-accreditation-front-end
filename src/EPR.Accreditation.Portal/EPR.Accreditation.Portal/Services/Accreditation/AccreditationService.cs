namespace EPR.Accreditation.Portal.Services.Accreditation
{
    using System.Threading.Tasks;
    using AutoMapper;
    using EPR.Accreditation.Portal.Common.Dtos;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.Common.Enums;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;

    public class AccreditationService : IAccreditationService
    {
        private readonly IMapper _mapper;
        private readonly RESTservices.Interfaces.IHttpAccreditationService _httpAccreditationService;

        public AccreditationService(
            IMapper mapper,
            RESTservices.Interfaces.IHttpAccreditationService httpAccreditationService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpAccreditationService = httpAccreditationService ?? throw new ArgumentNullException(nameof(httpAccreditationService));
        }

        public async Task<OperatorTypeViewModel> GetOperatorType(Guid id)
        {
            var result = await _httpAccreditationService.GetOperatorType(id);
            return new OperatorTypeViewModel
            {
                Id = id,
                OperatorType = result
            };
        }

        public async Task<Guid> CreateAccreditation(OperatorTypeViewModel viewModel)
        {
            var accreditation = new Accreditation
            {
                OperatorTypeId = viewModel.OperatorType.Value
            };

            var externalId = await _httpAccreditationService.CreateAccreditation(accreditation);
            return externalId;
        }

        public async Task<WasteLicensesAndPermitsViewModel> GetWastePermitViewModel(Guid id)
        {
            var wastePermit = await _httpAccreditationService.GetWastePermit(id);

            WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel = new WasteLicensesAndPermitsViewModel();

            if (wastePermit != null)
            {
                wasteLicensesAndPermitsViewModel = _mapper.Map<WasteLicensesAndPermitsViewModel>(wastePermit);
                wasteLicensesAndPermitsViewModel.Id = id;
            }

            return wasteLicensesAndPermitsViewModel;
        }

        public async Task SaveWastePermit(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel)
        {
            var wastePermit = _mapper.Map<DTOs.WastePermit.LicensesAndPermitsReferences>(wasteLicensesAndPermitsViewModel);

            await _httpAccreditationService.CreateWastePermit(wasteLicensesAndPermitsViewModel.Id, wastePermit);
        }

        public async Task<TaskListViewModel> GetTaskList(Guid id)
        {
            var taskStatus = await _httpAccreditationService.GetAccreditationTaskProgress(id);

            var viewModel = new TaskListViewModel
            {
                // Address = need the address from legal contacts and contact details
                Id = id,
                WasteLicensesStatus = ReturnStatusFromList(taskStatus, Enums.TaskName.WasteLicencesAndPrns).ToString(),
                UploadBusinessPlanStatus = ReturnStatusFromList(taskStatus, Enums.TaskName.UploadBusinessPlan).ToString(),
                AboutMaterialStatus = ReturnStatusFromList(taskStatus, Enums.TaskName.AboutMaterial).ToString(),
                UploadSupportingDocumentStatus = ReturnStatusFromList(taskStatus, Enums.TaskName.UploadSupportingDocuments).ToString(),
            };
            return viewModel;
        }

        public async Task<CheckYourAnswersViewModel> CheckYourAnswers(Guid id)
        {
            var result = await _httpAccreditationService.GetCheckYourAnswers(id);
            var vm = _mapper.Map<CheckYourAnswersViewModel>(result);
            return vm;
        }

        /// <summary>
        /// Determines if the ID is for an exporter or not
        /// </summary>
        /// <param name="id">The id of the accreditation</param>
        /// <returns>true if an exporter, otherwise false</returns>
        public async Task<bool> IsExporter(Guid id)
        {
            var operatorType = await _httpAccreditationService.GetOperatorType(id);

            return operatorType == OperatorType.Exporter;
        }

        /// <summary>
        /// Gets PRN tonnage data view.
        /// </summary>
        /// <param name="accreditationExternalId">Accreditation id.</param>
        /// <returns>PRN tonnage data view model.</returns>
        public async Task<PrnTonnesPlannedViewModel> GetPrnTonnesPlanned(Guid accreditationExternalId)
        {
            var result = await _httpAccreditationService.GetPrnTonnesPlanned(accreditationExternalId);
            var vm = _mapper.Map<PrnTonnesPlannedViewModel>(result);
            return vm;
        }

        /// <summary>
        /// Updates PRN tonnage data.
        /// </summary>
        /// <param name="accreditationExternalId">Accreditation id.</param>
        /// <param name="vm">View model for PRN tonnage data.</param>
        /// <returns>Returns completed Task.</returns>
        public async Task UpdatePrnTonnesPlanned(Guid accreditationExternalId, PrnTonnesPlannedViewModel vm)
        {
            var dto = _mapper.Map<PrnTonnesPlannedDto>(vm);
            await _httpAccreditationService.UpdatePrnTonnesPlanned(accreditationExternalId, dto);
        }

        /// <summary>
        /// Creates the view model for the given accreditation id
        /// </summary>
        /// <param name="id">The id of the accreditation that the legal documents are for</param>
        /// <returns>The view model for the accreditation legal documents</returns>
        public async Task<LegalDocumentsAddressViewModel> GetLegalDocumentsAddressViewModel(Guid id)
        {
            var addressDto = await _httpAccreditationService.GetLegalDocumentsAddress(id);

            return _mapper.Map<LegalDocumentsAddressViewModel>(addressDto);
        }

        /// <summary>
        /// Creates or updates the address for the legal documents for the accreditation
        /// </summary>
        /// <param name="id">Id of the accreditation id</param>
        /// <param name="viewModel">The view model containing the address for the legal documents</param>
        /// <returns>async task</returns>
        public async Task UpdateLegalDocumentsAddress(
            LegalDocumentsAddressViewModel viewModel)
        {
            var addressDto = _mapper.Map<AddressDto>(viewModel);

            await _httpAccreditationService.UpdateLegalDocumentsAddress(
                viewModel.Id,
                addressDto);
        }

        private Enums.TaskStatus ReturnStatusFromList(
            List<AccreditationTaskProgress> accreditationsTaskProgress,
            Enums.TaskName taskName)
        {
            if (accreditationsTaskProgress.Where(a => a.TaskNameId.ToString().Contains(taskName.ToString())).Any())
            {
                return (Enums.TaskStatus)Enum.Parse(typeof(Enums.TaskStatus), accreditationsTaskProgress.Where(a => a.TaskNameId.ToString().Contains(taskName.ToString())).FirstOrDefault().TaskStatusId.ToString());
            }

            return Enums.TaskStatus.NotStarted;
        }

        public async Task<CheckAnswersViewModel> CheckAnswers(
            Guid id,
            Guid materialId,
            CheckAnswersSection section)
        {
            var result = await _httpAccreditationService.GetCheckAnswers(
                id,
                materialId,
                section);

            var viewModel = _mapper.Map<CheckAnswersViewModel>(result);

            return viewModel;
        }
    }
}