using EPR.Accreditation.Portal.DTOs;
using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Portal.ViewModels;
using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    public interface IAccreditationService
    {
        Task<OperatorTypeViewModel> GetOperatorType(Guid id);
        Task<Guid> CreateAccreditation(OperatorTypeViewModel viewModel);
        Task<WasteLicensesAndPermitsViewModel> GetWastePermitViewModel(Guid id);

        Task SaveWastePermit(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewModel);
        Task<TaskListViewModel> GetTaskList(Guid id, Guid siteId, Guid materialId);
        Task<CheckYourAnswersViewModel> CheckYourAnswers(Guid id);
        Task<HasOverseasAgentViewModel> GetHasOverseasAgent(Guid id);
        Task SetOverseasAgentFlag(HasOverseasAgentViewModel viewModel);
    }
}
