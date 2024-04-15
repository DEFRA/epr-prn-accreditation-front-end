namespace EPR.Accreditation.Portal.Services.Accreditation
{
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using System;
    using System.Threading.Tasks;

    public class OverseasSiteService : IOverseasSiteService
    {
        public async Task<ReprocessorDetailsViewModel> GetReprocessorDetailsViewModel(Guid id)
        {
            //var reprocessorDetailsDto = await _httpOverseasSiteService.GetReprocessorDetails(id);

            return new ReprocessorDetailsViewModel
            {
                Id = id,
                OrganisationName = "Sample Ltd",
                Countries = new List<string>
                {
                    "Albania",
                    "France",
                    "Holland",
                    "Spain"
                },
                Address = "123 High street, London, WC1 6UH"
            };
        }

        public Task UpdateReprocessorDetails(ReprocessorDetailsViewModel reprocessorDetailsViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
