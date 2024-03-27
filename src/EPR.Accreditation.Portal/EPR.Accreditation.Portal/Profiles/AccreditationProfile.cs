using AutoMapper;
using EPR.Accreditation.Portal.DTOs;
using EPR.Accreditation.Portal.ViewModels;

namespace EPRN.Accreditation.Profiles
{
    public class AccreditationProfile : Profile
    {
        public AccreditationProfile()
        {
            CreateMap<EPR.Accreditation.Portal.DTOs.WastePermit, WasteLicensesAndPermitsViewModel>();
                
            CreateMap<WasteLicensesAndPermitsViewModel, EPR.Accreditation.Portal.DTOs.WastePermit>();
        }
    }
}