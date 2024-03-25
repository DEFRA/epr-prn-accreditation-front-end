using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    public interface IAccreditationService
    {
        Task<OperatorTypeViewModel> GetOperatorType(Guid id);
        Task UpdateOperatorType(OperatorTypeViewModel viewModel);

    }
}
