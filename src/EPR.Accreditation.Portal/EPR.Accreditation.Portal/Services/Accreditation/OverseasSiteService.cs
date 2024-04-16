namespace EPR.Accreditation.Portal.Services.Accreditation
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;
    using EPR.Accreditation.Portal.Services.Accreditation.Interfaces;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class OverseasSiteService : IOverseasSiteService
    {
        private readonly IHttpOverseasSiteService _httpOverseasSiteService;
        private readonly IMapper _mapper;

        public async Task<ReprocessorDetailsViewModel> GetReprocessorDetailsViewModel(
            Guid id,
            Guid overseasSiteId)
        {
            //var reprocessorDetailsDto = await _httpOverseasSiteService.GetReprocessorDetails(id, overseasSiteId);

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

        public async Task UpdateReprocessorDetails(ReprocessorDetailsViewModel reprocessorDetailsViewModel)
        {
            var reprocessorDetailsDto = _mapper.Map<DTOs.OverseasSite.ReprocessorDetailsDto>(reprocessorDetailsViewModel);

            await _httpOverseasSiteService.UpdateReprocessorDetails(
                reprocessorDetailsViewModel.Id,
                reprocessorDetailsViewModel.OverseasSiteId,
                reprocessorDetailsDto);
        }
    }
}
