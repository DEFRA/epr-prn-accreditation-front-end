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

        /// <summary>
        /// Adds a new uploaded file.
        /// </summary>
        /// <param name="accreditationId">Accreditation Id.</param>
        /// <returns>Completed task</returns>
        Task AddFile(Guid accreditationId);

        /// <summary>
        /// Deletes a previously uploaded file.
        /// </summary>
        /// <param name="accreditationId">Accreditation Id.</param>
        /// <param name="fileId">File Id.</param>
        /// <returns>Completed task</returns>
        Task DeleteFile(Guid accreditationId, Guid fileId);
    }
}
