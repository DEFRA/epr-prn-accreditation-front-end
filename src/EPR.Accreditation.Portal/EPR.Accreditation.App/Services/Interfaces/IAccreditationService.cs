namespace EPR.Accreditation.App.Services.Interfaces
{
    public interface IAccreditationService
    {
        Task<ApplicationSavedViewModel> GetApplicationSavedViewModel(int id);
    }
}
