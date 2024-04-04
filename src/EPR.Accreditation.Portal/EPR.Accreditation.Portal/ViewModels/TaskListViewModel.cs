using EPR.Accreditation.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Portal.ViewModels
{
    public class TaskListViewModel
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public Guid MaterialId { get; set; }

        public string Address { get; set; }
    }
}