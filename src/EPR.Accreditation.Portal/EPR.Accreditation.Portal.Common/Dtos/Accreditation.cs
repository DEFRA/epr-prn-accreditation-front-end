namespace EPR.Accreditation.Portal.Common.Dtos
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Accreditation.
    /// </summary>
    public class Accreditation
    {
        /// <summary>
        /// Gets or sets externalId.
        /// </summary>
        public Guid? ExternalId { get; set; } // This has a unique key added via the dbcontext

        /// <summary>
        /// Gets or sets OperatorTypeId.
        /// </summary>
        public Enums.OperatorType OperatorTypeId { get; set; }

        /// <summary>
        /// Gets or sets ReferenceNumber
        /// </summary>
        [MaxLength(12)]
        public string ReferenceNumber { get; set; } // This has a unique key added via the dbcontext

        /// <summary>
        /// Gets or sets OrganisationId
        /// </summary>
        public Guid OrganisationId { get; set; }

        /// <summary>
        /// Gets or sets Large.
        /// </summary>
        public bool? Large { get; set; } // Currently this means is it for above 400 tonnes or not

        /// <summary>
        /// Gets or sets AccreditationStatusId.
        /// </summary>
        public Enums.AccreditationStatus? AccreditationStatusId { get; set; }

        /// <summary>
        /// Gets or sets SiteId.
        /// </summary>
        public int? SiteId { get; set; }

        /// <summary>
        /// Gets or sets CreatedBy.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets CreatedOn.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets UpdatedBy.
        /// </summary>
        public Guid? UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets UpdatedOn.
        /// </summary>
        public DateTime? UpdatedOn { get; set; }
    }
}