namespace EPR.Accreditation.Portal.Services.Accreditation
{
    using System.Threading.Tasks;
    using AutoMapper;
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Facade.Common.Enums;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;

    public class AccreditationService : IAccreditationService
    {
        private readonly IMapper _mapper;
        protected readonly EPR.Accreditation.Portal.RESTservices.Interfaces.IHttpAccreditationService _httpAccreditationService;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public AccreditationService(IMapper mapper,
            EPR.Accreditation.Portal.RESTservices.Interfaces.IHttpAccreditationService httpAccreditationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _httpAccreditationService = httpAccreditationService ?? throw new ArgumentNullException(nameof(httpAccreditationService));

        }

        private Enums.TaskStatus ReturnStatusFromList(List<AccreditationTaskProgress> accreditationsTaskProgress,
                                                Enums.TaskName taskName)
        {
            if (accreditationsTaskProgress.Where(a => a.TaskNameId.ToString().Contains(taskName.ToString())).ToList().Count > 0)
            {
                return (Enums.TaskStatus)Enum.Parse(typeof(Enums.TaskStatus), accreditationsTaskProgress.Where(a => a.TaskNameId.ToString().Contains(taskName.ToString())).FirstOrDefault().TaskStatusId.ToString());
            }
            return Enums.TaskStatus.NotStarted;
        }

        public async Task<OperatorTypeViewModel> GetOperatorType(Guid id)
        {
            var result = await _httpAccreditationService.GetOperatorType(id);
            return new OperatorTypeViewModel { ExternalId = id, OperatorType = result };
        }

        public async Task<Guid> CreateAccreditation(OperatorTypeViewModel viewModel)
        {
            var accreditation = new Facade.Common.Dtos.Accreditation { OperatorTypeId = viewModel.OperatorType.Value };
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

        public async Task<TaskListViewModel> GetTaskList(Guid id, 
                                                        Guid siteId, 
                                                        Guid materialId)
        {
            var taskStatus = await _httpAccreditationService.GetAccreditationTaskProgress(id);
            var address = await _httpAccreditationService.GetSite(siteId);

            var viewModel = new TaskListViewModel
            {
                Id = id,
                SiteId = siteId,
                MaterialId = materialId,
                Address = address.Address1.ToString(),
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

        public async Task<OverseasReprocessingSiteOutputsViewModel> GetOverseasReprocessingSiteOutputs(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId)
        {
            var overseasSiteOutputs = await _httpAccreditationService.GetOverseasReprocessingSiteOutputs(
                accreditationExternalId,
                overseasSiteExternalId);
            return _mapper.Map<OverseasReprocessingSiteOutputsViewModel>(overseasSiteOutputs);
        }

        public async Task UpdateOverseasReprocessingSiteOutputs(
            Guid accreditationExternalId,
            OverseasReprocessingSiteOutputsViewModel overseasSiteOutputsViewModel)
        {
            var overseasSiteOutputs = _mapper.Map<OverseasReprocessingSiteOutputs>(overseasSiteOutputsViewModel);
            await _httpAccreditationService.UpdateOverseasReprocessingSiteOutputs(accreditationExternalId, overseasSiteOutputs);
        }
    }
}