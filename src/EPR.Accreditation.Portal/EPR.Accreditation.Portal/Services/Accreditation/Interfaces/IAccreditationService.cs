namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    using EPR.Accreditation.Portal.ViewModels;

    public interface IAccreditationService
    {
        Task<OperatorTypeViewModel> GetOperatorType(Guid id);

        Task<Guid> CreateAccreditation(OperatorTypeViewModel viewModel);

        Task<WasteLicensesAndPermitsViewModel> GetWastePermitViewModel(Guid id);

        Task SaveWastePermit(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel);

        Task<TaskListViewModel> GetTaskList(Guid id, Guid siteId, Guid materialId);

        Task<CheckYourAnswersViewModel> CheckYourAnswers(Guid id);

        /// <summary>
        /// Defines the function for getting the view model for
        /// the overseas reprocessor view model
        /// </summary>
        /// <param name="id">The id of the accreditation</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation that contains the view model</returns>
        Task<object> GetOverseasReprocessorViewModel(Guid id);
    }
}
