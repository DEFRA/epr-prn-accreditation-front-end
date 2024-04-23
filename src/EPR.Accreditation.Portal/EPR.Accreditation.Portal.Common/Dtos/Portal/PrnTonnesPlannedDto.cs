namespace EPR.Accreditation.Portal.Common.Dtos.Portal
{
    using System;
    using EPR.Accreditation.Portal.Common.Enums;

    public class PrnTonnesPlannedDto
    {
        public Guid ExternalId { get; set; }

        public PrnPlannedTonnesType? PrnPlannedTonnesType { get; set; }

        public decimal? PrnPlannedTonnesFee { get; set; }
    }
}
