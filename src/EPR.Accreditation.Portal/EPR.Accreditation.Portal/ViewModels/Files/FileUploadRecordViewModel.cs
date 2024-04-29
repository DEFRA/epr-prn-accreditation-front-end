namespace EPR.Accreditation.Portal.ViewModels.Files
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// A view model for uploaded file data.
    /// </summary>
    public class FileUploadRecordViewModel
    {
        /// <summary>
        /// Gets or sets the file Id.
        /// </summary>
        public Guid? FileId { get; set; }

        /// <summary>
        /// Gets or sets a file name.
        /// </summary>
        [MaxLength(50)]
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets when the file was uploaded.
        /// </summary>
        public DateTime DateUploaded { get; set; }

        /// <summary>
        /// Gets or sets who uploaded the file.
        /// </summary>
        [MaxLength(50)]
        public string UploadedBy { get; set; }
    }
}
