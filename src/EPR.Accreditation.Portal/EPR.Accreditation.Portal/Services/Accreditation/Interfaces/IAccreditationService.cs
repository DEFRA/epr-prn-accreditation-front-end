namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    using EPR.Accreditation.Portal.ViewModels;

    public interface IAccreditationService
    {
        Task<OperatorTypeViewModel> GetOperatorType(Guid id);

        Task<Guid> CreateAccreditation(OperatorTypeViewModel viewModel);

        Task<WasteLicensesAndPermitsViewModel> GetWastePermitViewModel(Guid id);

        Task SaveWastePermit(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel);

        Task<TaskListViewModel> GetTaskList(Guid id);

        Task<CheckYourAnswersViewModel> CheckYourAnswers(Guid id);

        /// <summary>
        /// Determines if the ID is for an exporter or not
        /// </summary>
        /// <param name="id">The id of the accreditation</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation that contains the view model</returns>
        Task<bool> IsExporter(Guid id);

        Task<PrnTonnesPlannedViewModel> GetPrnTonnesPlanned(Guid accreditationExternalId);

        Task UpdatePrnTonnesPlanned(Guid accreditationExternalId, PrnTonnesPlannedViewModel vm);

        /// <summary>
        /// Creates the view model for the given accreditation id
        /// </summary>
        /// <param name="id">The id of the accreditation that the legal documents are for</param>
        /// <returns>The view model for the accreditation legal documents</returns>
        Task<LegalDocumentsAddressViewModel> GetLegalDocumentsAddressViewModel(Guid id);

        /// <summary>
        /// Creates or updates the address for the legal documents for the accreditation
        /// </summary>
        /// <param name="viewModel">The view model containing the address for the legal documents</param>
        /// <returns>async task</returns>
        Task UpdateLegalDocumentsAddress(LegalDocumentsAddressViewModel viewModel);

        /// <summary>
        /// Returns the Completion view model.
        /// </summary>
        /// <param name="id">The accrediation id</param>
        /// <returns>CompletionViewModel</returns>
        Task<CompletionViewModel> Completion(Guid id);
    }
}
