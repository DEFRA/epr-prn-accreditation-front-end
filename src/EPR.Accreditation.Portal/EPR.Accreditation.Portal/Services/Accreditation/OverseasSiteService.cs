namespace EPR.Accreditation.Portal.Services.Accreditation
{
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Mvc.Rendering;
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
                Countries = new List<SelectListItem>
                {
                    new() { Value = string.Empty, Text = ReprocessorDetailsResources.DefaultOption },
                    new() { Value = "1", Text = "Albania" },
                    new() { Value = "2", Text = "France" },
                    new() { Value = "3", Text = "Holland" },
                    new() { Value = "4", Text = "Spain" },
                    new() { Value = "5", Text = "Switzerland" }
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
