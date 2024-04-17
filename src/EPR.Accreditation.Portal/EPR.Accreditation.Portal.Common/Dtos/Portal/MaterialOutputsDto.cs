namespace EPR.Accreditation.Portal.Common.Dtos.Portal
{
    public class MaterialOutputsDto
    {
        public bool? WasteLastYear { get; set; }

        public decimal? TonnesNotProcessedOnSite { get; set; }

        public decimal? TonnesContaminents { get; set; }

        public decimal? TonnesProcessLoss { get; set; }
    }
}
