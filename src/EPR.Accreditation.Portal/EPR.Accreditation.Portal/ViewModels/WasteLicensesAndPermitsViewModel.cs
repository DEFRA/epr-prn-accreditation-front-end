namespace EPR.Accreditation.Portal.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;

    /// <summary>
    /// Defining the view model
    /// </summary>
    public class WasteLicensesAndPermitsViewModel
    {
        /// <summary>
        /// Gets or sets the accreditation ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the DealerRegistrationNumber with the required validation attributes
        /// </summary>
        [RegularExpression(@"^[a-zA-Z0-9/]*$", ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "ErrorMessageInvalidFormat")]
        [StringLength(20, ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "ErrorMessageTooLong")]
        public string DealerRegistrationNumber { get; set; }

        /// <summary>
        /// Gets or sets the EnvironmentalPermitNumber with the required validation attributes
        /// </summary>
        [RegularExpression(@"^[a-zA-Z0-9/]*$", ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "ErrorMessageInvalidFormat")]
        [StringLength(20, ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "ErrorMessageTooLong")]
        public string EnvironmentalPermitNumber { get; set; }

        /// <summary>
        /// Gets or sets the PartBActivityReferenceNumber with the required validation attributes
        /// </summary>
        [RegularExpression(@"^[a-zA-Z0-9/]*$", ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "ErrorMessageInvalidFormat")]
        [StringLength(20, ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "ErrorMessageTooLong")]
        public string PartBActivityReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the PartAActivityReferenceNumber with the required validation attributes
        /// </summary>
        [RegularExpression(@"^[a-zA-Z0-9/]*$", ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "ErrorMessageInvalidFormat")]
        [StringLength(20, ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "ErrorMessageTooLong")]
        public string PartAActivityReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the DischargeConsentNumber with the required validation attributes
        /// </summary>
        [RegularExpression(@"^[a-zA-Z0-9/]*$", ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "ErrorMessageInvalidFormat")]
        [StringLength(20, ErrorMessageResourceType = typeof(WasteLicensesAndPermitsResources), ErrorMessageResourceName = "ErrorMessageTooLong")]
        public string DischargeConsentNumber { get; set; }
    }
}
