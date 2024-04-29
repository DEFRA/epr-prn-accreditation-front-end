namespace EPR.Accreditation.Portal.ViewModels.Files
{
    using EPR.Accreditation.Portal.Controllers;

    /// <summary>
    /// A view model for sending uploaded files data to a view.
    /// </summary>
    public class FileUploadRecordsViewModel
    {
        /// <summary>
        /// Gets or sets the Accreditation Id.
        /// </summary>
        public Guid AccreditationId { get; set; }

        /// <summary>
        /// Gets or sets a List of files previously uploaded (Flow charts).
        /// </summary>
        public List<FileUploadRecordViewModel> FlowCharts { get; set; } = new List<FileUploadRecordViewModel>();

        /// <summary>
        /// Gets or sets a List of files previously uploaded (Recording systems).
        /// </summary>
        public List<FileUploadRecordViewModel> RecordingSystems { get; set; } = new List<FileUploadRecordViewModel>();

        /// <summary>
        /// Gets or sets a List of files previously uploaded (Plants and equiments used to reprocess material).
        /// </summary>
        public List<FileUploadRecordViewModel> PlantsAndEquipments { get; set; } = new List<FileUploadRecordViewModel>();

        /// <summary>
        /// Gets or sets a List of files previously uploaded (Sampling and inspection).
        /// </summary>
        public List<FileUploadRecordViewModel> SamplingAndInspection { get; set; } = new List<FileUploadRecordViewModel>();
    }
}
