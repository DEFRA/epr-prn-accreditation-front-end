using AutoMapper;
using EPR.Accreditation.Portal.Common.Dtos.Portal;
using EPR.Accreditation.Portal.DTOs.MaterialReprocessorDetails;
using EPR.Accreditation.Portal.DTOs.Site;
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

            CreateMap<SiteAddressViewModel, Site>().
                ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
