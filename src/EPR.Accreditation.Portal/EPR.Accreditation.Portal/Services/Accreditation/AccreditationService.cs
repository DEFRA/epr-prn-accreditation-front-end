using AutoMapper;
using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;
using System.Threading.Tasks;


namespace EPR.Accreditation.Portal.Services.Accreditation
{
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

        private Enums.TaskStatus returnStatusFromList(List<AccreditationTaskProgress> accreditationsTaskProgress,
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
                WasteLicensesStatus = returnStatusFromList(taskStatus, Enums.TaskName.WasteLicencesAndPrns).ToString(),
                UploadBusinessPlanStatus = returnStatusFromList(taskStatus, Enums.TaskName.UploadBusinessPlan).ToString(),
                AboutMaterialStatus = returnStatusFromList(taskStatus, Enums.TaskName.AboutMaterial).ToString(),
                UploadSupportingDocumentStatus = returnStatusFromList(taskStatus, Enums.TaskName.UploadSupportingDocuments).ToString(),
            };
            return viewModel;
        }
    }
}