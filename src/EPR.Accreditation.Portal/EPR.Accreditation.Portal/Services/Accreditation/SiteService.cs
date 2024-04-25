namespace EPR.Accreditation.Portal.Services.Accreditation
{
    using AutoMapper;
    using EPR.Accreditation.Portal.DTOs.Site;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;

    /// <summary>
    /// SiteService.
    /// </summary>
    public class SiteService : ISiteService
    {
        /// <summary>
        /// _mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// _httpSiteService.
        /// </summary>
        private readonly RESTservices.Interfaces.IHttpSiteService _httpSiteService;

        /// <summary>
        /// _httpContextAccessor.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteService"/> class.
        /// SiteService.
        /// </summary>
        /// <param name="mapper">mapper.</param>
        /// <param name="httpSiteService">httpSiteService.</param>
        /// <param name="httpContextAccessor">httpContextAccessor.</param>
        /// <exception cref="ArgumentNullException">ArgumentNullException.</exception>
        public SiteService(
            IMapper mapper,
            RESTservices.Interfaces.IHttpSiteService httpSiteService,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _httpSiteService = httpSiteService ?? throw new ArgumentNullException(nameof(httpSiteService));
        }

        /// <summary>
        /// GetSiteAddressViewModel
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>Task<SiteAddressViewModel></returns>
        public async Task<SiteAddressViewModel> GetSiteAddressViewModel(
            Guid id)
        {
            SiteAddressViewModel siteAddressViewModel = new SiteAddressViewModel();

            try
            {
                var site = await _httpSiteService.GetSite(id);
                siteAddressViewModel = _mapper.Map<SiteAddressViewModel>(site);
            }
            finally
            {
                siteAddressViewModel.Id = id;
            }

            return siteAddressViewModel;
        }

        /// <summary>
        /// SaveSiteAddress
        /// </summary>
        /// <param name="siteAddressViewModel">siteAddressViewModel</param>
        /// <returns>Task</returns>
        public async Task SaveSiteAddress(SiteAddressViewModel siteAddressViewModel)
        {
            Site site = null;
            var siteAddress = _mapper.Map<Site>(siteAddressViewModel);
            try
            {
                site = await _httpSiteService.GetSite(siteAddressViewModel.Id);
            }
            finally
            {
                if (site == null)
                {
                    // This has to be updated when org Id is retrieved at login
                    siteAddress.OrganisationId = Guid.NewGuid();
                    await _httpSiteService.CreateSite(siteAddressViewModel.Id, siteAddress);
                }
                else
                {
                    await _httpSiteService.UpdateSite(siteAddressViewModel.Id, siteAddress);
                }
            }
        }
    }
}