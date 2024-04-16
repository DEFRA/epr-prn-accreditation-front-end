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

        Task<OverseasReprocessingSiteOutputsViewModel> GetOverseasReprocessingSiteOutputs(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId);

        Task UpdateOverseasReprocessingSiteOutputs(
            OverseasReprocessingSiteOutputsViewModel overseasSiteOutputs);
    }
}
