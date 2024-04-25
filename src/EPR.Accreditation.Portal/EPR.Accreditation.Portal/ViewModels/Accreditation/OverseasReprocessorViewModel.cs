namespace EPR.Accreditation.Portal.ViewModels.Accreditation
{
    using System.ComponentModel.DataAnnotations;
    using EPR.Accreditation.Portal.Resources;

    /// <summary>
    /// View model for representing the selection of the adding
    /// overseas reprocessing site now or later
    /// </summary>
    public class OverseasReprocessorViewModel
    {
        /// <summary>
        /// Gets or sets the id of the accreditation
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether the user wants to
        /// add an overseas reprocessing site now or later
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(OverseasReprocessorResources), ErrorMessageResourceName = "SelectionError")]
        public bool? AddOverseasReprocessor { get; set; }
    }
}