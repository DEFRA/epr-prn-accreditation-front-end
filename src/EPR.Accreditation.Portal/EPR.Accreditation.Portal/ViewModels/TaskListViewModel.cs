namespace EPR.Accreditation.Portal.ViewModels
{
    public class TaskListViewModel
    {
        public Guid Id { get; set; }

        public Guid MaterialId { get; set; }

        public string Address { get; set; }

        public string WasteLicensesStatus { get; set; }

        public string UploadBusinessPlanStatus { get; set; }

        public string AboutMaterialStatus { get; set; }

        public string UploadSupportingDocumentStatus { get; set; }
    }
}