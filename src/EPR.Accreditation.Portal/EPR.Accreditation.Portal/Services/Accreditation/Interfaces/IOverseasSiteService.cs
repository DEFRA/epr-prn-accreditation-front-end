namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    using EPR.Accreditation.Portal.ViewModels;

    /// <summary>
    /// Interface for the overseas site serivice
    /// </summary>
    public interface IOverseasSiteService
    {
        /// <summary>
        /// Gets the view model to drive the view
        /// </summary>
        /// <param name="id">Acceditation ID</param>
        /// <param name="overseasSiteId">Overseas site ID</param>
        /// <returns>The view model to the view</returns>
        Task<ReprocessorDetailsViewModel> GetReprocessorDetailsViewModel(
            Guid id,
            Guid overseasSiteId);

        /// <summary>
        /// Updates the overseas reprocessor details using the view model
        /// </summary>
        /// <param name="reprocessorDetailsViewModel">The view model submitted</param>
        /// <returns>Task completed asynchronously</returns>
        Task UpdateReprocessorDetails(ReprocessorDetailsViewModel reprocessorDetailsViewModel);

        /// <summary>
        /// Gets the view model to drive the view
        /// </summary>
        /// <param name="accreditationExternalId">Accreditation Id.</param>
        /// <param name="overseasSiteExternalId">Overseas site Id.</param>
        /// <returns>The view model to the view.</returns>
        Task<OverseasReprocessingSiteOutputsViewModel> GetOverseasReprocessingSiteOutputs(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId);

        /// <summary>
        /// Updates the overseas reprocessor output using the view model.
        /// </summary>
        /// <param name="overseasSiteOutputs">The view model submitted.</param>
        /// <returns>Task completed asynchronously.</returns>
        Task UpdateOverseasReprocessingSiteOutputs(
            OverseasReprocessingSiteOutputsViewModel overseasSiteOutputs);
    }
}
