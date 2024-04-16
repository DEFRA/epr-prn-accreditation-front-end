namespace EPR.Accreditation.Portal.RESTservices
{
    using EPR.Accreditation.Portal.Common.RESTservices;
    using EPR.Accreditation.Portal.DTOs.OverseasSite;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;

    public class HttpOverseasSiteService : BaseHttpService, IHttpOverseasSiteService
    {
        public HttpOverseasSiteService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName)
            : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task<ReprocessorDetailsDto> GetReprocessorDetails(
            Guid id,
            Guid overseasSiteId)
        {
            return await Get<ReprocessorDetailsDto>($"{id}/OverseasSite/{overseasSiteId}/ReprocessorDetails");
        }

        public async Task UpdateReprocessorDetails(
            Guid id,
            Guid overseasSiteId,
            ReprocessorDetailsDto reprocessorDetails)
        {
            await Put($"{id}/OverseasSite/{overseasSiteId}/ReprocessorDetails", reprocessorDetails);
        }
    }
}
