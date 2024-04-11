using AutoMapper;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation
{
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
            var address = await _httpSiteService.GetSite(id, siteId);

            //var siteAddress = _mapper.Map<SiteAddressViewModel>(address);

            SiteAddressViewModel siteAddressViewModel = new SiteAddressViewModel()
            {
                Id = id,
                AddressLine1 = address.Address1,
                AddressLine2 = address.Address2,
                TownOrCity = address.Town,
                County = address.County,
                PostCode = address.Postcode
            };

            return siteAddressViewModel;
        }

        public async Task SaveSiteAddress(SiteAddressViewModel siteAddressViewModel)
        {
            var siteAddress = _mapper.Map<DTOs.Site.Site>(siteAddressViewModel);

            siteAddress.Address1 = siteAddressViewModel.AddressLine1;
            siteAddress.Address2 = siteAddressViewModel.AddressLine2;
            siteAddress.Town = siteAddressViewModel.TownOrCity;
            siteAddress.County = siteAddressViewModel.County;
            siteAddress.Postcode = siteAddressViewModel.PostCode;


            await _httpSiteService.CreateSite(siteAddressViewModel.Id, siteAddress);
        }
    }
}