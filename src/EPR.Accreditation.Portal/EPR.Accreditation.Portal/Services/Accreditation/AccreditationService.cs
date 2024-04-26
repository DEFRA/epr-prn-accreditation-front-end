namespace EPR.Accreditation.Portal.Services.Accreditation
{
    using System.Threading.Tasks;
    using AutoMapper;
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Portal.Common.Dtos;
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

        public async Task<TaskListViewModel> GetTaskList(
            Guid id,
            Guid materialId)
        {
            var taskStatus = await _httpAccreditationService.GetAccreditationTaskProgress(id);

            var viewModel = new TaskListViewModel
            {
                Id = id,
                MaterialId = materialId,

                // Address = need the address from legal contacts and contact details
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
    }
}