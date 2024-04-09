namespace EPR.Accreditation.Portal.Common.Dtos.Portal
{
    public class NonWasteInputsDto
    {
        public Guid Id { get; set; }

        public Guid MaterialId { get; set; }

        public bool? WasteLastYear { get; set; }

        public IEnumerable<NonWasteInputRecordDto> NonWasteInputRecords { get; set; }
    }

    public class NonWasteInputRecordDto
    {
        public string Type { get; set; }

        public decimal? Tonnes { get; set; }
    }
}
