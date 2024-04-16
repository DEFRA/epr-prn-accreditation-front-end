namespace EPR.Accreditation.Portal.Common.Dtos.Portal
{
    public class ReprocessingSupportingInformationDto
    {
        public Guid Id { get; set; }

        public Guid MaterialId { get; set; }

        public bool? WasteLastYear { get; set; }

        public IEnumerable<ReprocessingSupportingInformationRecordDto> Records { get; set; }
    }
}
