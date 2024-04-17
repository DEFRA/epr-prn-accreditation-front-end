namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Facade.Common.Enums;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.DTOs.WastePermit;
    using DTO = EPR.Accreditation.Facade.Common.Dtos;

    public interface IHttpAccreditationService
    {
        Task CreateWastePermit(Guid accreditationId, LicensesAndPermitsReferences wastePermit);

        Task<LicensesAndPermitsReferences> GetWastePermit(Guid accreditationId);

        Task<OperatorType> GetOperatorType(Guid accreditationExternalId);

        Task<Guid> CreateAccreditation(DTO.Accreditation accreditation);

        Task<DTO.AccreditationMaterial> GetAccreditationMaterial(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId);

        Task UpdateAccreditationMaterial(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            DTO.AccreditationMaterial accreditationMaterial);

        Task<CheckYourAnswersDto> GetCheckYourAnswers(Guid accreditationExternalId);

        Task<Site> GetSite(Guid siteId);

        Task<List<AccreditationTaskProgress>> GetAccreditationTaskProgress(Guid accreditationExternalId);

        Task<DTO.OverseasReprocessingSite> GetOverseasSite(
            Guid accreditationExternalId,
            Guid siteExternalId);

        Task<HasOverseasAgentDto> GetHasOverseasAgent(Guid accreditationExternalId);

        Task SetHasOverseasAgent(Guid accreditationExternalId, bool? hasOverseasAgent);
    }
}