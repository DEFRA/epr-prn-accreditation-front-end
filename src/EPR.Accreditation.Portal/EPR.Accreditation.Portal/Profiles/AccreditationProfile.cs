using EPR.Accreditation.Portal.ViewModels.CheckAnswers;

namespace EPR.Accreditation.Portal.Profiles
{
    using AutoMapper;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using DTOs.MaterialReprocessorDetails;
    using DTOs.OverseasSite;
    using DTOs.WastePermit;
    using ViewModels;
    using ViewModels.SiteMaterial;

    /// <summary>
    /// Class to describe mappings of view models and DTOs
    /// </summary>
    public class AccreditationProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccreditationProfile"/> class.
        /// </summary>
        public AccreditationProfile()
        {
            CreateMap<LicensesAndPermitsReferences, WasteLicensesAndPermitsViewModel>();
            CreateMap<WasteLicensesAndPermitsViewModel, LicensesAndPermitsReferences>();
            CreateMap<PermitExemptionViewModel, PermitExemption>();
            CreateMap<ReprocessedWasteLastYearViewModel, ReprocessedWasteLastYear>();
            CreateMap<ReprocessorDetailsViewModel, ReprocessorDetailsDto>();
            CreateMap<MaterialOutputsDto, MaterialOutputsViewModel>()
                .ReverseMap();

            CreateMap<ReprocessingSupportingInformationDto, NonWasteInputsViewModel>()
                .ForMember(d => d.Rows, o => o.MapFrom(s => s.Records ?? new List<ReprocessingSupportingInformationRecordDto>()))
                .ReverseMap()
                .ForMember(d => d.Records, o => o.MapFrom(s => s.Rows));

            CreateMap<ReprocessingSupportingInformationRecordDto, TypeTonnesRowViewModel>()
                .ReverseMap();

            CreateMap<CheckYourAnswersDto, CheckYourAnswersViewModel>();
            
            CreateMap<CheckAnswersDto, CheckAnswersViewModel>();            

            CreateMap<MaterialWasteOutputsDto, MaterialWasteOutputsViewModel>()
                .ReverseMap();

            CreateMap<ReprocessingSupportingInformationDto, ProductsProducedViewModel>()
                .ForMember(d => d.Rows, o => o.MapFrom(s => s.Records ?? new List<ReprocessingSupportingInformationRecordDto>()))
                .ReverseMap()
                .ForMember(d => d.Records, o => o.MapFrom(s => s.Rows));
        }
    }
}
