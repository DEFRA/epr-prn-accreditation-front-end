namespace EPR.Accreditation.Portal.Common.Dtos
{
    /// <summary>
    /// AccreditationTaskProgress.
    /// </summary>
    public class AccreditationTaskProgress
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets TaskStatusId.
        /// </summary>
        public Enums.TaskStatus TaskStatusId { get; set; }

        /// <summary>
        /// Gets or sets TaskNameId.
        /// </summary>
        public Enums.TaskName TaskNameId { get; set; }
    }
}