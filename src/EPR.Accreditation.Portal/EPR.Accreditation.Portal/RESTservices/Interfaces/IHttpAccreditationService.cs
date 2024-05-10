namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
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

        /// <summary>
        /// Updates the reference number.
        /// </summary>
        /// <param name="accreditationId">The accrediation id.</param>
        /// <returns></returns>
        Task UpdateReferenceNumber(Guid accreditationId);

        Task<string> GetReferenceNumber(Guid id);

        Task<decimal> GetAccreditationFee(Guid id);

        Task<PrnTonnesPlannedDto> GetPrnTonnesPlanned(Guid accreditationExternalId);

        Task UpdatePrnTonnesPlanned(Guid accreditationExternalId, PrnTonnesPlannedDto dto);

        /// <summary>
        /// Gets the legal documents address for the accreditation
        /// </summary>
        /// <param name="id">The id of the accreditation</param>
        /// <returns>The DTO Address object</returns>
        Task<AddressDto> GetLegalDocumentsAddress(Guid id);

        /// <summary>
        /// Saves the address for the legal documents
        /// </summary>
        /// <param name="id">Id of the accreditation for the legal documents</param>
        /// <param name="address">The DTO address object</param>
        /// <returns>async task</returns>
        Task UpdateLegalDocumentsAddress(
            Guid id,
            AddressDto address);

        /// <summary>
        /// Gets the completion record.
        /// </summary>
        /// <param name="id">The a</param>
        /// <returns></returns>
        Task<Completion> GetCompletion(Guid id);
    }
}