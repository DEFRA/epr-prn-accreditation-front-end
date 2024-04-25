namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    using EPR.Accreditation.Portal.ViewModels;

    public interface IAccreditationService
    {
        Task<OperatorTypeViewModel> GetOperatorType(Guid id);

        Task<Guid> CreateAccreditation(OperatorTypeViewModel viewModel);

        Task<WasteLicensesAndPermitsViewModel> GetWastePermitViewModel(Guid id);

        Task SaveWastePermit(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel);

        Task<TaskListViewModel> GetTaskList(
            Guid id,
            Guid materialId);

        Task<CheckYourAnswersViewModel> CheckYourAnswers(Guid id);

        /// <summary>
        /// Determines if the ID is for an exporter or not
        /// </summary>
        /// <param name="id">The id of the accreditation</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation that contains the view model</returns>
        Task<bool> IsExporter(Guid id);
    }
}
