namespace EPR.Accreditation.Portal.RESTservices
{
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Portal.Common.Dtos;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.Common.Enums;
    using EPR.Accreditation.Portal.Common.RESTservices;
    using EPR.Accreditation.Portal.DTOs.WastePermit;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;

    public class HttpAccreditationService : BaseHttpService, IHttpAccreditationService
    {
        public HttpAccreditationService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName)
            : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
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
            return await Get<OperatorType>($"{accreditationExternalId}/OperatorType");
        }

        public async Task<Guid> CreateAccreditation(Accreditation accreditation)
        {
            var externalId = await Post<Guid>(
                string.Empty,
                accreditation);
            return externalId;
        }

        public async Task<AccreditationMaterial> GetAccreditationMaterial(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId)
        {
            return await Get<AccreditationMaterial>($"{accreditationExternalId}/Site/{siteExternalId}/Material/{materialExternalId}");
        }

        public async Task UpdateAccreditationMaterial(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            AccreditationMaterial accreditationMaterial)
        {
            await Put($"{accreditationExternalId}/Site/{siteExternalId}/Material/{materialExternalId}", accreditationMaterial);
        }

        public async Task<CheckYourAnswersDto> GetCheckYourAnswers(Guid accreditationExternalId)
        {
            return await Get<CheckYourAnswersDto>($"{accreditationExternalId}/CheckYourAnswers");
        }

        public async Task<Site> GetSite(
            Guid accreditationExternalId)
        {
            return await Get<Site>($"{accreditationExternalId}/Site");
        }

        public async Task<List<AccreditationTaskProgress>> GetAccreditationTaskProgress(Guid accreditationExternalId)
        {
            return await Get<List<AccreditationTaskProgress>>($"{accreditationExternalId}/TaskProgress");
        }

        public async Task<OverseasReprocessingSiteOutputs> GetOverseasReprocessingSiteOutputs(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId)
        {
            return await Get<OverseasReprocessingSiteOutputs>($"{accreditationExternalId}/OverseasSite/{overseasSiteExternalId}/Outputs");
        }

        public async Task UpdateOverseasReprocessingSiteOutputs(
            Guid accreditationExternalId,
            OverseasReprocessingSiteOutputs overseasSiteOutputs)
        {
            await Put($"{accreditationExternalId}/OverseasSite/Outputs", overseasSiteOutputs);
        }
    }
}
