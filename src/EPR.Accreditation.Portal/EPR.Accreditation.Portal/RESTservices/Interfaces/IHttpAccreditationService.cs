namespace EPR.Accreditation.Portal.RESTservices.Interfaces
{
    using EPR.Accreditation.Portal.Common.Dtos;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.Common.Enums;
    using EPR.Accreditation.Portal.DTOs.WastePermit;
    using EPR.Accreditation.Portal.ViewModels.Files;
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

        Task<PrnTonnesPlannedDto> GetPrnTonnesPlanned(Guid accreditationExternalId);

        Task UpdatePrnTonnesPlanned(Guid accreditationExternalId, PrnTonnesPlannedDto dto);

        /// <summary>
        /// Gets a list of file uploads for the given accreditaion.
        /// </summary>
        /// <param name="accreditationId">Accreditation Id.</param>
        /// <returns>A list of FileUpload objects.</returns>
        Task<List<FileUpload>> GetFiles(Guid accreditationId);

        /// <summary>
        /// Adds a new uploaded file.
        /// </summary>
        /// <param name="accreditationId">Accreditation Id.</param>
        /// <returns>Completed task</returns>
        Task<FileUploadRecordsViewModel> AddFile(Guid accreditationId);

        /// <summary>
        /// Deletes a previously uploaded file.
        /// </summary>
        /// <param name="accreditationId">Accreditation Id.</param>
        /// <param name="fileId">File Id.</param>
        /// <returns>Completed task</returns>
        Task DeleteFile(Guid accreditationId, Guid fileId);
    }
}