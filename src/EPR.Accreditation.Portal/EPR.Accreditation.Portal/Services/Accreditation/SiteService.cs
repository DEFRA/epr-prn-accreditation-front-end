namespace EPR.Accreditation.Portal.Services.Accreditation
{
    using AutoMapper;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;

    public class SiteService : ISiteService
    {
        private readonly IMapper _mapper;
        protected readonly EPR.Accreditation.Portal.RESTservices.Interfaces.IHttpSiteService _httpSiteService;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public SiteService(IMapper mapper,
            EPR.Accreditation.Portal.RESTservices.Interfaces.IHttpSiteService httpSiteService,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _httpSiteService = httpSiteService ?? throw new ArgumentNullException(nameof(httpSiteService));
        }

        public async Task<SiteAddressViewModel> GetSiteAddressViewModel(Guid id, Guid siteId, Guid materialId)
        {
            SiteAddressViewModel siteAddressViewModel = new SiteAddressViewModel()
            {
                Id = id,
                SiteId = siteId,
                MaterialId = materialId
            };

            return siteAddressViewModel;
        }

        public async Task SaveSiteAddress(SiteAddressViewModel siteAddressViewModel)
        {
            var siteAddress = _mapper.Map<DTOs.Site.Site>(siteAddressViewModel);
            if (siteAddress.Id == null || siteAddress.Id == 0)
            {
                siteAddress.OrganisationId = Guid.NewGuid();
                await _httpSiteService.CreateSite(siteAddressViewModel.Id, siteAddress);
            }
            else
            {
                siteAddress.OrganisationId = siteAddress.OrganisationId;
                siteAddress.SiteAuthorties = siteAddress.SiteAuthorties;
                await _httpSiteService.UpdateSite(siteAddressViewModel.Id, siteAddress);
            }
        }
    }
}