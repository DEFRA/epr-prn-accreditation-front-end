namespace EPR.Accreditation.Portal.ViewModels
{
    public class CheckYourAnswersViewModel
    {
        public Guid Id { get; set; }

        public bool Completed { get; set; }

        public string SiteAddress { get; set; }

        public string WasteCarrierRegistrationNumber { get; set; }

        public string WasteManagementLicenceNumber { get; set; }

        public string PartAReferenceNumber { get; set; }

        public string PartBReferenceNumber { get; set; }

        public string DischargeConsentNumber { get; set; }

        public string ExemptionReferenceNumber { get; set; }

        public string PeopleOfAuthority { get; set; }
    }
}
