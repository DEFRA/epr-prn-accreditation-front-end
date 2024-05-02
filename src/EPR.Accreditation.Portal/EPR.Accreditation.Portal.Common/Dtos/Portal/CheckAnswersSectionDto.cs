namespace EPR.Accreditation.Portal.Common.Dtos.Portal
{
    public class CheckAnswersSectionDto
    {
        //public Guid Id { get; set; }

        public string Title { get; set; }

        public bool Completed { get; set; }

        public List<CheckAnswersSectionRowDto> SectionRows { get; set; }

        //public Dictionary<string, string> QueryStringRouteData { get; set; } = new();
    }
}