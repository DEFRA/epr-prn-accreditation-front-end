using AutoMapper;
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
        }
    }
}
