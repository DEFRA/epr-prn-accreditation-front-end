namespace EPR.Accreditation.Portal.RESTservices
{
    using EPR.Accreditation.Portal.Common.Dtos;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.Common.Enums;
    using EPR.Accreditation.Portal.Common.RESTservices;
    using EPR.Accreditation.Portal.DTOs.WastePermit;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;

    /// <summary>
    /// Initialises the class
    /// </summary>
    public class HttpAccreditationService : BaseHttpService, IHttpAccreditationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpAccreditationService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">httpContextAccessor interface to implement</param>
        /// <param name="httpClientFactory">httpClientFactory interface to implement</param>
        /// <param name="baseUrl">The base URL</param>
        /// <param name="endPointName">The name of the endpoint</param>
        public HttpAccreditationService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName)
            : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        /// <summary>
        /// Method to create the waste permit using the provided data
        /// </summary>
        /// <param name="id">Accreditation ID</param>
        /// <param name="wastePermit">The relevant DTO</param>
        /// <returns>Task completed asynchronously</returns>
        public async Task CreateWastePermit(
            Guid id,
            LicencesAndPermitsReferences wastePermit)
        {
            await Post($"{id}/WastePermit", wastePermit);
        }

        /// <summary>
        /// Method to get the Waste Permit data
        /// </summary>
        /// <param name="id">Accreditation ID</param>
        /// <returns>The relevant DTO</returns>
        public async Task<LicencesAndPermitsReferences> GetWastePermit(Guid id)
        {
            return await Get<LicencesAndPermitsReferences>($"{id}/WastePermit");
        }

        public async Task<OperatorType> GetOperatorType(Guid id)
        {
            return await Get<OperatorType>($"{id}/OperatorType");
        }

        public async Task<Guid> CreateAccreditation(Accreditation accreditation)
        {
            var externalId = await Post<Guid>(
                string.Empty,
                accreditation);
            return externalId;
        }

        public async Task<CheckYourAnswersDto> GetCheckYourAnswers(Guid id)
        {
            return await Get<CheckYourAnswersDto>($"{id}/CheckYourAnswers");
        }

        public async Task<Site> GetSite(
            Guid id)
        {
            return await Get<Site>($"{id}/Site");
        }

        public async Task<List<AccreditationTaskProgress>> GetAccreditationTaskProgress(Guid id)
        {
            return await Get<List<AccreditationTaskProgress>>($"{id}/TaskProgress");
        }

        /// <summary>
        /// Gets PRN tonnage data for given accreditation.
        /// </summary>
        /// <param name="accreditationExternalId">Accreditation id.</param>
        /// <returns>PRN tonnage data dto.</returns>
        public async Task<PrnTonnesPlannedDto> GetPrnTonnesPlanned(Guid accreditationExternalId)
        {
            return await Get<PrnTonnesPlannedDto>($"{accreditationExternalId}/PrnTonnesPlanned");
        }

        /// <summary>
        /// Updates PRN tonnage data for given accreditation.
        /// </summary>
        /// <param name="accreditationExternalId">Accreditation id.</param>
        /// <param name="dto">PRN data dto.</param>
        /// <returns>Completed Task.</returns>
        public async Task UpdatePrnTonnesPlanned(
            Guid accreditationExternalId,
            PrnTonnesPlannedDto dto)
        {
            await Put($"{accreditationExternalId}/PrnTonnesPlanned", dto);
        }

        /// <summary>
        /// Requests the legal documents address from the facade
        /// </summary>
        /// <param name="id">The id of the accreditation</param>
        /// <returns>The address dto object</returns>
        public async Task<AddressDto> GetLegalDocumentsAddress(Guid id)
        {
            return await Get<AddressDto>($"{id}/LegalDocumentAddress");
        }

        /// <summary>
        /// Sends the dto to the facade for saving
        /// </summary>
        /// <param name="id">The id of the accreditation</param>
        /// <param name="address">The address DTO</param>
        /// <returns>async task</returns>
        public async Task UpdateLegalDocumentsAddress(
            Guid id,
            AddressDto address)
        {
            await Put($"{id}/LegalDocumentAddress", address);
        }
    }
}