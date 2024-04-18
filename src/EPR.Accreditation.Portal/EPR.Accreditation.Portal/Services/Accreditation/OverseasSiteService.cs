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

    /// <summary>
    /// This service provides the controller with a view model for the GET and updates reprocessor details for the POST
    /// </summary>
    public class OverseasSiteService : IOverseasSiteService
    {
        private readonly IHttpOverseasSiteService _httpOverseasSiteService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OverseasSiteService"/> class.
        /// Constructor for OverseasSiteService
        /// </summary>
        /// <param name="httpOverseasSiteService">httpOverseasSiteService object</param>
        /// <param name="httpCountryService">httpCountryService object</param>
        /// <param name="mapper">An instance of the mapper</param>
        public OverseasSiteService(
            IHttpOverseasSiteService httpOverseasSiteService,
            IMapper mapper)
        {
            _httpOverseasSiteService = httpOverseasSiteService ?? throw new ArgumentNullException(nameof(httpOverseasSiteService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// GET request to retrive the view model to drive the view
        /// </summary>
        /// <param name="id">This is the ID of the accreditation</param>
        /// <param name="overseasSiteId">This is the overseas reprocessing site ID</param>
        /// <returns>Returns the view model asynchronously</returns>
        public async Task<ReprocessorDetailsViewModel> GetReprocessorDetailsViewModel(
            Guid id,
            Guid overseasSiteId)
        {
            var reprocessorDetailsDto = await _httpOverseasSiteService.GetReprocessorDetails(id, overseasSiteId);

            var countries = new List<SelectListItem>
            {
                new()
                {
                    Value = string.Empty,
                    Text = ReprocessorDetailsResources.DefaultOption
                }
            };

            foreach (var country in reprocessorDetailsDto.CountryList)
            {
                countries.Add(
                    new()
                    {
                        Value = country.CountryId.ToString(),
                        Text = country.Name
                    });
            }

            return new ReprocessorDetailsViewModel
            {
                Id = id,
                Name = reprocessorDetailsDto.Name,
                Countries = countries,
                Address = reprocessorDetailsDto.Address,
                CountryId = reprocessorDetailsDto.CountryId.ToString()
            };
        }

        /// <summary>
        /// POST method to update the reprocessor details based on the submitted forms view model
        /// </summary>
        /// <param name="reprocessorDetailsViewModel">The view model that comes from the form</param>
        /// <returns>>An Ok result</returns>
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
