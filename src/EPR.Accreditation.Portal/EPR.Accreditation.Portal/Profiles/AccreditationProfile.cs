using AutoMapper;
using EPR.Accreditation.Portal.Common.Dtos;
using EPR.Accreditation.Portal.Common.Dtos.Portal;
using EPR.Accreditation.Portal.DTOs.MaterialReprocessorDetails;
using EPR.Accreditation.Portal.DTOs.WastePermit;
using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Profiles
{
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
                .ForMember(d => d.Rows, o => o.MapFrom(s => s.NonWasteInputRecords))
                .ReverseMap();

            CreateMap<NonWasteInputRecordDto, NonWasteInputRow>()
                .ReverseMap();
        }
    }
}
