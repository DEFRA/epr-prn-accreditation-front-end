namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    using EPR.Accreditation.Portal.DTOs.OverseasSite;

    /// <summary>
    /// Interface for the Http overseas site service
    /// </summary>
    public interface IHttpOverseasSiteService
    {
        /// <summary>
        /// Gets the reprocessor details
        /// </summary>
        /// <param name="id">Accreditation ID</param>
        /// <param name="overseasSiteId">Overseas site ID</param>
        /// <returns>The reprocessor details as a DTO</returns>
        Task<ReprocessorDetailsDto> GetReprocessorDetails(
            Guid id,
            Guid overseasSiteId);

        /// <summary>
        /// Updates the reprocessor details
        /// </summary>
        /// <param name="id">Accreditation ID</param>
        /// <param name="overseasSiteId">Overseas site ID</param>
        /// <param name="reprocessorDetails">The updated reprocessor details as a DTO</param>
        /// <returns>Task completed asynchronously</returns>
        Task UpdateReprocessorDetails(
            Guid id,
            Guid overseasSiteId,
            ReprocessorDetailsDto reprocessorDetails);
    }
}
