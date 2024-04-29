namespace EPR.Accreditation.Portal.Services.FileService
{
    using EPR.Accreditation.Portal.ViewModels.Files;

    /// <summary>
    /// Interface for the file service class
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Gets a list of uploaded file records.
        /// </summary>
        /// <param name="accreditationId">Accreditation Id.</param>
        /// <returns>A list of file uploaded view models</returns>
        Task<FileUploadRecordsViewModel> GetFileRecords(Guid accreditationId);
    }
}
