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
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.PermitNumber, o => o.MapFrom(s => s.EnvironmentalPermitNumber))
                .ForMember(d => d.DischargeConstentNumber, o => o.MapFrom(s => s.DischargeConsentNumber))
                .ForMember(d => d.RegistrationNumber, o => o.MapFrom(s => s.DealerRegistrationNumber))
                .ForMember(d => d.ActivityReferenceNumber, o => o.MapFrom(s => s.PartAActivityReferenceNumber))
                .ForMember(d => d.ActivityNumber, o => o.MapFrom(s => s.PartBActivityReferenceNumber))
                ;

            CreateMap<WasteLicensesAndPermitsViewModel, EPR.Accreditation.Portal.DTOs.WastePermit>()
                .ForMember(s => s.Id, opt => opt.Ignore())
                .ForMember(d => d.EnvironmentalPermitNumber, o => o.MapFrom(s => s.PermitNumber))
                .ForMember(d => d.DischargeConsentNumber, o => o.MapFrom(s => s.DischargeConstentNumber))
                .ForMember(d => d.DealerRegistrationNumber, o => o.MapFrom(s => s.RegistrationNumber))
                .ForMember(d => d.PartAActivityReferenceNumber, o => o.MapFrom(s => s.ActivityReferenceNumber))
                .ForMember(d => d.PartBActivityReferenceNumber, o => o.MapFrom(s => s.ActivityNumber))
                ;
        }
    }
}