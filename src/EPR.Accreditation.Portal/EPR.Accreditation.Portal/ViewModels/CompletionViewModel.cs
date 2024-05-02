namespace EPR.Accreditation.Portal.ViewModels
{
    public class CompletionViewModel
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public Guid MaterialId { get; set; }

        public string CountryCode { get; set; }

        public decimal AmountDue { get; set; }

        public string ReferenceNumber { get; set; }
    }
}