namespace EPR.Accreditation.Portal.Services.FileService
{
    using EPR.Accreditation.Portal.ViewModels.Files;

    /// <summary>
    /// A class containing methods to handle uploaded file management
    /// </summary>
    public class FileService : IFileService
    {
        /// <summary>
        /// Gets a list of uploaded file records.
        /// </summary>
        /// <param name="accreditationId">Accreditation Id.</param>
        /// <returns>A list of file uploaded view models</returns>
        public async Task<FileUploadRecordsViewModel> GetFileRecords(Guid accreditationId)
        {
            var fileUploadRecordsViewModel = new FileUploadRecordsViewModel();

            fileUploadRecordsViewModel.FlowCharts.Add(new FileUploadRecordViewModel
            {
                FileId = accreditationId,
                Filename = "Some File 1.csv",
                DateUploaded = DateTime.Now.AddDays(-2),
                UploadedBy = "Some User"
            });

            return fileUploadRecordsViewModel;
        }
    }
}
