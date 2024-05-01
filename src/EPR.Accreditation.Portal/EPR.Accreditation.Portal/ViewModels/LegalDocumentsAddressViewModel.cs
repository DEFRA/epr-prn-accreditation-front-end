namespace EPR.Accreditation.Portal.ViewModels
{
    /// <summary>
    /// View model that represents the data required for the
    /// address for legal documents
    /// </summary>
    public class LegalDocumentsAddressViewModel : AddressViewModel
    {
        /// <summary>
        /// Gets or sets the accreditatrion id the model is for
        /// </summary>
        public Guid Id { get; set; }
    }
}
