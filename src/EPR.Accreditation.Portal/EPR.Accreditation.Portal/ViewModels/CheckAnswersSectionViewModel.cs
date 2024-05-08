namespace EPR.Accreditation.Portal.ViewModels
{
    public class CheckAnswersSectionViewModel
    {
        //public Guid Id { get; set; }

        public string Title { get; set; }

        public bool Completed { get; set; }

        public List<CheckAnswersSectionRowViewModel> SectionRows { get; set; }

        //public Dictionary<string, string> QueryStringRouteData { get; set; } = new();
    }
}
