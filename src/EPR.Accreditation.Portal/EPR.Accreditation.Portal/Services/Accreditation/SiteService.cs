using AutoMapper;
using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;

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

        public async Task<Site> GetSite(Guid id, Guid siteId)
        {
            var site = await _httpSiteService.GetSite(id, siteId);

            return site;
        }
    }
}