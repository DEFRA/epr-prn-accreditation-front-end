namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    using EPR.Accreditation.Portal.Common.Dtos;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.Common.Enums;
    using EPR.Accreditation.Portal.DTOs.WastePermit;
    using DTO = EPR.Accreditation.Portal.Common.Dtos;

    public interface IHttpAccreditationService
    {
        Task CreateWastePermit(Guid accreditationId, LicensesAndPermitsReferences wastePermit);

        Task<LicensesAndPermitsReferences> GetWastePermit(Guid accreditationId);

        Task<OperatorType> GetOperatorType(Guid accreditationExternalId);

        Task<Guid> CreateAccreditation(DTO.Accreditation accreditation);

        Task<CheckYourAnswersDto> GetCheckYourAnswers(Guid accreditationExternalId);

        Task<Site> GetSite(Guid siteId);

        Task<List<AccreditationTaskProgress>> GetAccreditationTaskProgress(Guid accreditationExternalId);

        Task<PrnTonnesPlannedDto> GetPrnTonnesPlanned(Guid accreditationExternalId);

        Task UpdatePrnTonnesPlanned(Guid accreditationExternalId, PrnTonnesPlannedDto dto);
    }
}