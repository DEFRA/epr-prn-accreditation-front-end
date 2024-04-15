namespace EPR.Accreditation.Portal.Profiles
{
    using AutoMapper;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.DTOs.MaterialReprocessorDetails;
    using EPR.Accreditation.Portal.DTOs.WastePermit;
    using EPR.Accreditation.Portal.ViewModels;

    public class AccreditationProfile : Profile
    {
        public AccreditationProfile()
        {
            this.CreateMap<LicensesAndPermitsReferences, WasteLicensesAndPermitsViewModel>();
            this.CreateMap<WasteLicensesAndPermitsViewModel, LicensesAndPermitsReferences>();
            this.CreateMap<PermitExemptionViewModel, PermitExemption>();
            this.CreateMap<ReprocessedWasteLastYearViewModel, ReprocessedWasteLastYear>();
            this.CreateMap<MaterialOutputsDto, MaterialOutputsViewModel>()
                .ReverseMap();

            this.CreateMap<NonWasteInputsDto, NonWasteInputsViewModel>()
                    .ForMember(d => d.Rows, o => o.MapFrom(s => s.NonWasteInputRecords ?? new List<NonWasteInputRecordDto>()))
                    .ReverseMap()
                    .ForMember(d => d.NonWasteInputRecords, o => o.MapFrom(s => s.Rows));

            this.CreateMap<NonWasteInputRecordDto, NonWasteInputsRowViewModel>()
                .ReverseMap();
        }
    }
}
