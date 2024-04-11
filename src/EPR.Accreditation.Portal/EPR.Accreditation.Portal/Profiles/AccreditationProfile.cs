namespace EPR.Accreditation.Portal.Profiles
{
    using AutoMapper;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.DTOs.MaterialReprocessorDetails;
    using EPR.Accreditation.Portal.DTOs.WastePermit;
    using EPR.Accreditation.Portal.ViewModels;
    using EPR.Accreditation.Portal.ViewModels.SiteMaterial;

    public class AccreditationProfile : Profile
    {
        public AccreditationProfile()
        {
            CreateMap<LicensesAndPermitsReferences, WasteLicensesAndPermitsViewModel>();
            CreateMap<WasteLicensesAndPermitsViewModel, LicensesAndPermitsReferences>();
            CreateMap<PermitExemptionViewModel, PermitExemption>();
            CreateMap<ReprocessedWasteLastYearViewModel, ReprocessedWasteLastYear>();
            CreateMap<MaterialOutputsDto, MaterialOutputsViewModel>()
                .ReverseMap();

            CreateMap<NonWasteInputsDto, NonWasteInputsViewModel>()
                .ForMember(d => d.Rows, o => o.MapFrom(s => s.NonWasteInputRecords ?? new List<NonWasteInputRecordDto>()))
                .ReverseMap()
                .ForMember(d => d.NonWasteInputRecords, o => o.MapFrom(s => s.Rows));

            CreateMap<NonWasteInputRecordDto, TypeTonnesRowViewModel>()
                .ReverseMap();

            CreateMap<NonWasteInputsDto, ProductsProducedViewModel>()
                .ForMember(d => d.Rows, o => o.MapFrom(s => s.NonWasteInputRecords ?? new List<NonWasteInputRecordDto>()))
                .ReverseMap()
                .ForMember(d => d.NonWasteInputRecords, o => o.MapFrom(s => s.Rows));
        }
    }
}
