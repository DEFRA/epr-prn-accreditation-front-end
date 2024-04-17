namespace EPR.Accreditation.Portal.Common.Dtos.Portal
{
    public class MaterialWasteOutputsDto
    {
        public bool? WasteLastYear { get; set; }

        public decimal? UkPackagingWaste { get; set; }

        public decimal? NonUkPackagingWaste { get; set; }

        public decimal? NonPackagingWaste { get; set; }
    }
}
