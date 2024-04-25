namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Portal.Common.Dtos;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.Common.Enums;
    using EPR.Accreditation.Portal.DTOs.WastePermit;
    using DTO = EPR.Accreditation.Portal.Common.Dtos;

    public interface IHttpAccreditationService
    {
        Task CreateWastePermit(
            Guid id,
            LicensesAndPermitsReferences wastePermit);

        Task<LicensesAndPermitsReferences> GetWastePermit(Guid id);

        Task<OperatorType> GetOperatorType(Guid id);

        Task<Guid> CreateAccreditation(DTO.Accreditation accreditation);

        Task<CheckYourAnswersDto> GetCheckYourAnswers(Guid id);

        Task<Site> GetSite(Guid siteId);

        Task<List<AccreditationTaskProgress>> GetAccreditationTaskProgress(Guid id);
    }
}