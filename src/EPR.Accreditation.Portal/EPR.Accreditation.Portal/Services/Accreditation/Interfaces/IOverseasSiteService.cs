namespace EPR.Accreditation.Portal.Services.Accreditation.Interfaces
{
    using EPR.Accreditation.Portal.ViewModels;

    public interface IOverseasSiteService
    {
        Task<ReprocessorDetailsViewModel> GetReprocessorDetailsViewModel(Guid id);

        Task UpdateReprocessorDetails(ReprocessorDetailsViewModel reprocessorDetailsViewModel);
    }
}
