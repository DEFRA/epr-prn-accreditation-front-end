using EPR.Accreditation.Portal.Common.Dtos.Portal;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class CheckAnswersViewModel
    {
        public Guid Id { get; set; }

        public bool Completed { get; set; }

        public AddressDto SiteAddress { get; set; }

        public List<CheckAnswersSectionViewModel> Sections { get; set; }
    }
}
