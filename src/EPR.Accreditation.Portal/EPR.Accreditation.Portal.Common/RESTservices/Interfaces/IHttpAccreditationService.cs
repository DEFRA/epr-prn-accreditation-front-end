using EPR.Accreditation.Facade.Common.Enums;
using DTO = EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpAccreditationService
    {

        Task<OperatorType> GetOperatorType(Guid accreditationExternalId);
        Task UpdateOperatorType(
            Guid accreditationExternalId, 
            OperatorType operatorTypeId);

        Task<DTO.AccreditationMaterial> GetAccreditationMaterial(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId);
        Task UpdateAccreditationMaterial(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            DTO.AccreditationMaterial accreditationMaterial);
    }
}
