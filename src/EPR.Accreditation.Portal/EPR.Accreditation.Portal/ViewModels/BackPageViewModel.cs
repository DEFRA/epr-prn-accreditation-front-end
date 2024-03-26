namespace EPR.Accreditation.Portal.ViewModels
{
    public class BackPageViewModel
    {
        public bool HasBackLink => !string.IsNullOrWhiteSpace(Url);

        public string Url { get; set; }
    }
}