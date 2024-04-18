namespace EPR.Accreditation.Portal.ViewModels.CheckAnswers
{
    public class CheckAnswersViewModel
    {
        public Guid Id { get; set; }

        public bool Completed { get; set; }

        public string SiteAddress { get; set; }

        public List<CheckAnswersRowViewModel> SectionRows { get; set; } = new();

        public Dictionary<string, string> QueryStringRouteData { get; set; } = new();
    }
}
