﻿using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    public interface IAccreditationService
    {
        ApplicationSavedViewModel GetApplicationSavedViewModel(Guid id);

        WasteLicensesAndPermitsViewModel GetWasteLicensesAndPermitsViewModel(Guid id);

        Task<Task> SaveWasteLicensesAndPermitsViewMode(WasteLicensesAndPermitsViewModel wasteLicensesAndPermitsViewMode);

        Task SaveAccreditation(Guid id, DTOs.Accreditation accreditation);
    }
}
