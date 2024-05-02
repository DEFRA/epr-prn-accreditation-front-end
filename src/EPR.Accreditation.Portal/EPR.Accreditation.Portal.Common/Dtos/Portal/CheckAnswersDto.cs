namespace EPR.Accreditation.Portal.Common.Dtos.Portal
{
    public class CheckAnswersDto
    {
        public Guid Id { get; set; }

        public bool Completed { get; set; }

        public AddressDto SiteAddress { get; set; }

        public List<CheckAnswersSectionDto> Sections { get; set; }
    }
}
