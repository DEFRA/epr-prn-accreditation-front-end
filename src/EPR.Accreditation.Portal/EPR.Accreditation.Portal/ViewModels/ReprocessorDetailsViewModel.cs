namespace EPR.Accreditation.Portal.ViewModels
{
    public class ReprocessorDetailsViewModel
    {
        public Guid Id { get; set; }

        public string OrganisationName { get; set; }

        public IEnumerable<string> Countries { get; set; }

        public string SelectedCountry { get; set; }

        public string Address { get; set; }
    }
}
