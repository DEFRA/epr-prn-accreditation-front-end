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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool?> GetHasAccreditationNum(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hasAccreditationNum"></param>
        /// <returns></returns>
        Task UpdateHasAccreditationNum(
            Guid id,
            bool hasAccreditationNum);
    }
}