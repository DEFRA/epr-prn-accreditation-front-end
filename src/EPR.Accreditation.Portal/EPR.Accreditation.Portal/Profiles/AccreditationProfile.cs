using AutoMapper;
using EPR.Accreditation.Portal.DTOs;
using EPR.Accreditation.Portal.ViewModels;

namespace EPRN.Accreditation.Profiles
{
    public class AccreditationProfile : Profile
    {
        public AccreditationProfile()
        {
            CreateMap<EPR.Accreditation.Portal.DTOs.WastePermit, WasteLicensesAndPermitsViewModel>()
                .ForMember(d => d.Id, opt => opt.Ignore());

            CreateMap<WasteLicensesAndPermitsViewModel, EPR.Accreditation.Portal.DTOs.WastePermit>()
                .ForMember(s => s.Id, opt => opt.Ignore());
        }
    }
}