namespace EPR.Accreditation.Portal.RESTservices
{
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Portal.Common.RESTservices;
    using EPR.Accreditation.Portal.DTOs.OverseasSite;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;

    /// <summary>
    /// Class for the Http overseas site service
    /// </summary>
    public class HttpOverseasSiteService : BaseHttpService, IHttpOverseasSiteService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpOverseasSiteService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">Injecting the context accessor</param>
        /// <param name="httpClientFactory">Injecting the client factory</param>
        /// <param name="baseUrl">Declaring the base URL</param>
        /// <param name="endPointName">Declaring the name of the endpoint</param>
        public HttpOverseasSiteService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName)
            : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        /// <summary>
        /// Gets the reprocessor details from the Facade
        /// </summary>
        /// <param name="id">Accreditation ID</param>
        /// <param name="overseasSiteId">Overseas site ID</param>
        /// <returns>The overseas reprocessor details</returns>
        public async Task<ReprocessorDetailsDto> GetReprocessorDetails(
            Guid id,
            Guid overseasSiteId)
        {
            return await Get<ReprocessorDetailsDto>($"{id}/OverseasSite/{overseasSiteId}/ReprocessorDetails");
        }

        /// <summary>
        /// Updates the reprocesor details and passes it to the Facade
        /// </summary>
        /// <param name="id">Accreditation ID</param>
        /// <param name="overseasSiteId">overseas site ID</param>
        /// <param name="reprocessorDetails">The updated details as a DTO</param>
        /// <returns>Task completed asynchronously</returns>
        public async Task UpdateReprocessorDetails(
            Guid id,
            Guid overseasSiteId,
            ReprocessorDetailsDto reprocessorDetails)
        {
            await Put($"{id}/OverseasSite/{overseasSiteId}/ReprocessorDetails", reprocessorDetails);
        }

        public async Task<OverseasReprocessingSiteOutputs> GetOverseasReprocessingSiteOutputs(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId)
        {
            return await Get<OverseasReprocessingSiteOutputs>($"{accreditationExternalId}/OverseasSite/{overseasSiteExternalId}/Outputs");
        }

        public async Task UpdateOverseasReprocessingSiteOutputs(
            Guid accreditationExternalId,
            OverseasReprocessingSiteOutputs overseasSiteOutputs)
        {
            await Put($"{accreditationExternalId}/OverseasSite/{overseasSiteOutputs.ExternalId}/Outputs", overseasSiteOutputs);
        }
    }
}
