using EPR.Accreditation.Portal.Validation;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class NonWasteInputsViewModel
    {
        public Guid Id { get; set; }

        public Guid MaterialId { get; set; }

        public bool? WasteLastYear { get; set; }

        public IList<NonWasteInputRow> Rows { get; set; }
    }

    public class NonWasteInputRow
    {
        [RequireBothFields("Tonnes")]
        public string Type { get; set; }

        [RequireBothFields("Type")]
        public decimal? Tonnes { get; set; }
    }
}
