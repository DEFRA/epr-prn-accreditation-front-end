namespace EPR.Accreditation.Portal.ViewModels
{
    public class ExemptionReferencesViewModel
    {
        public Guid Id { get; set; }

        public IList<ExemptionReferenceViewModel> ExemptionReferencesVm { get; set; }

        //public IEnumerable<ExemptionReference> ExemptionReferences { get; set; }
    }
}
