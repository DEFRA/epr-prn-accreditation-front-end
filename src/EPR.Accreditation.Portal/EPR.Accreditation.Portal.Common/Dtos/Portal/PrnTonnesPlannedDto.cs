namespace EPR.Accreditation.Portal.Common.Dtos.Portal
{
    using EPR.Accreditation.Portal.Common.Enums;

    /// <summary>
    /// A DTO class to transfer PRN planned tonnage data.
    /// </summary>
    public class PrnTonnesPlannedDto
    {
        /// <summary>
        /// Gets PRN planned tonnage type.
        /// </summary>
        public PrnPlannedTonnesType? PrnPlannedTonnesType { get; set; }

        /// <summary>
        /// Gets PRN planned fee at the time of creating this entry.
        /// </summary>
        public decimal? PrnPlannedTonnesFee { get; set; }
    }
}
