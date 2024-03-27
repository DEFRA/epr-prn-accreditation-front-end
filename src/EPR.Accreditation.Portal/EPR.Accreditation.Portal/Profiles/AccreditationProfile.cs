using AutoMapper;
using EPR.Accreditation.Portal.DTOs.WastePermit;
using EPR.Accreditation.Portal.ViewModels;

namespace EPR.Accreditation.Portal.Profiles
{
    public class AccreditationProfile : Profile
    {
        public AccreditationProfile()
        {
            CreateMap<EPR.Accreditation.Portal.DTOs.WastePermit, WasteLicensesAndPermitsViewModel>();
                
            CreateMap<WasteLicensesAndPermitsViewModel, EPR.Accreditation.Portal.DTOs.WastePermit>();
            CreateMap<PermitExemptionViewModel, PermitExemption>();
        }
    }
}
