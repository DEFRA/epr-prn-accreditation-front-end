using AutoMapper;
using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;


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

        public async Task<TaskListViewModel> GetTaskList(Guid id, Guid siteId, Guid materialId)
        {
            var viewModel = new TaskListViewModel
            {
                Id = id,
                SiteId = siteId,
                MaterialId = materialId
            };
            return viewModel;
        }

        public async Task<Site> GetSite(Guid siteId)
        {
            var site = await _httpAccreditationService.GetSite(siteId);

            return site;
        }
    }
}