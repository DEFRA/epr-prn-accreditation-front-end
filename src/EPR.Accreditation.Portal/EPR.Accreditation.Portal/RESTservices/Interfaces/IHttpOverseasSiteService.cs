namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    using EPR.Accreditation.Portal.DTOs.OverseasSite;

    public interface IHttpOverseasSiteService
    {
        Task<ReprocessorDetailsDto> GetReprocessorDetails(
            Guid id,
            Guid overseasSiteId);

        Task UpdateReprocessorDetails(
            Guid id,
            Guid overseasSiteId,
            ReprocessorDetailsDto reprocessorDetails);
    }
}
