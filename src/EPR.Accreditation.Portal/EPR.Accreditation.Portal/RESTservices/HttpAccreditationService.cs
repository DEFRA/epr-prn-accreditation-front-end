using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Portal.DTOs.WastePermit;
using EPR.Accreditation.Portal.DTOs.Site;
using EPR.Accreditation.Portal.Common.Dtos;
using EPR.Accreditation.Portal.RESTservices.Interfaces;


namespace EPR.Accreditation.Portal.RESTservices
{
    public class HttpAccreditionService : BaseHttpService, IHttpAccreditationService
    {
        public HttpAccreditionService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName) : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task CreateWastePermit(
            Guid id, 
            LicensesAndPermitsReferences wastePermit)
        {
            await Post($"{id}/WastePermit", wastePermit);
        }
        
        public async Task<LicensesAndPermitsReferences> GetWastePermit(Guid id)
        {
           return await Get<LicensesAndPermitsReferences>($"{id}/WastePermit");
        }

        public async Task<OperatorType> GetOperatorType(Guid accreditationExternalId)
        {
            var operatorType = await Get<OperatorType>($"{accreditationExternalId}/OperatorType");
            return operatorType;
        }

        public async Task<Guid> CreateAccreditation(EPR.Accreditation.Facade.Common.Dtos.Accreditation accreditation)
        {
            var externalId = await Post<Guid>("", accreditation);
            return externalId;
        }

        public async Task<EPR.Accreditation.Facade.Common.Dtos.AccreditationMaterial> GetAccreditationMaterial(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId)
        {
            return await Get<EPR.Accreditation.Facade.Common.Dtos.AccreditationMaterial>($"{accreditationExternalId}/Site/{siteExternalId}/Material/{materialExternalId}");
        }

        public async Task UpdateAccreditationMaterial(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            AccreditationMaterial accreditationMaterial)
        {
            await Put($"{accreditationExternalId}/Site/{siteExternalId}/Material/{materialExternalId}", accreditationMaterial);
        }
    }
}
