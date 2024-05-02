namespace EPR.Accreditation.Portal.Common.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class FileUpload
    {
        public int Id { get; set; }

        public int AccreditationId { get; set; }

        [MaxLength(50)]
        public string Filename { get; set; }

        public Guid? FileId { get; set; }

        public DateTime DateUploaded { get; set; }

        [MaxLength(50)]
        public string UploadedBy { get; set; }

        public Enums.FileUploadType FileUploadTypeId { get; set; }

        public Enums.FileUploadStatus Status { get; set; }
    }
}
