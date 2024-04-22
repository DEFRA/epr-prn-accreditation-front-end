namespace EPR.Accreditation.Portal.Profiles
{
    using AutoMapper;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.DTOs.MaterialReprocessorDetails;
    using EPR.Accreditation.Portal.DTOs.WastePermit;
    using EPR.Accreditation.Portal.ViewModels;
    using EPR.Accreditation.Portal.ViewModels.SiteMaterial;

    /// <summary>
    /// Profile class for automapping maps
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
            CreateMap<MaterialOutputsDto, MaterialOutputsViewModel>()
                .ReverseMap();

            CreateMap<ReprocessingSupportingInformationDto, NonWasteInputsViewModel>()
                .ForMember(d => d.Rows, o => o.MapFrom(s => s.Records ?? new List<ReprocessingSupportingInformationRecordDto>()))
                .ReverseMap()
                .ForMember(d => d.Records, o => o.MapFrom(s => s.Rows));

            CreateMap<ReprocessingSupportingInformationRecordDto, TypeTonnesRowViewModel>()
                .ReverseMap();

            CreateMap<ReprocessingSupportingInformationDto, ProductsProducedViewModel>()
                .ForMember(d => d.Rows, o => o.MapFrom(s => s.Records ?? new List<ReprocessingSupportingInformationRecordDto>()))
                .ReverseMap()
                .ForMember(d => d.Records, o => o.MapFrom(s => s.Rows));

            CreateMap<string, WasteDescriptionCodeRowViewModel>()
                .ForMember(d => d.WasteDescriptionCode, o => o.MapFrom(s => s));
        }
    }
}
