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
        /// Gets whether a 2024 NPWD Accreditation number is present
        /// </summary>
        /// <param name="id">Accreditation ID</param>
        /// <returns>A boolean value</returns>
        Task<bool?> GetHasNpwdAccreditationNumber(Guid id);

        /// <summary>
        /// Updates the 2024 NPWD Accreditation Number
        /// </summary>
        /// <param name="id">Accreditation ID</param>
        /// <param name="hasAccreditationNum">The boolean value</param>
        /// <returns>Task completed asynchronously</returns>
        Task UpdateHasNpwdAccreditationNumber(
            Guid id,
            bool hasAccreditationNum);
    }
}