namespace EPR.Accreditation.UnitTests.RESTserviceTests
{
    using System.Net;
    using EPR.Accreditation.Portal.Common.Dtos.Portal;
    using EPR.Accreditation.Portal.DTOs.MaterialReprocessorDetails;
    using EPR.Accreditation.Portal.Enums;
    using EPR.Accreditation.Portal.RESTservices;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Moq.Protected;
    using Newtonsoft.Json;
    using Enums = EPR.Accreditation.Portal.Enums;

    [TestClass]
    public class HttpSiteMaterialServiceTests
    {
        private HttpSiteMaterialService _httpSiteMaterialService;
        private Mock<IHttpContextAccessor> _contextAccessor;
        private Mock<IHttpClientFactory> _httpClientFactory;
        private HttpClient _httpClient;
        private Mock<DelegatingHandler> _clientHandlerMock;
        private string _baseUrl = "http://baseUrl";
        private string _endpointName = "endpointName";
        private string _capturedUrl;
        private string _capturedPayload;

        public HttpSiteMaterialServiceTests()
        {
            _clientHandlerMock = new Mock<DelegatingHandler>();

            _clientHandlerMock.As<IDisposable>().Setup(s => s.Dispose());
            _httpClient = new HttpClient(_clientHandlerMock.Object);

            _contextAccessor = new Mock<IHttpContextAccessor>();
            _httpClientFactory = new Mock<IHttpClientFactory>(MockBehavior.Strict);

            _httpClientFactory.Setup(f => f.CreateClient(string.Empty)).Returns(_httpClient).Verifiable();
            _httpSiteMaterialService = new HttpSiteMaterialService(
                _contextAccessor.Object,
                _httpClientFactory.Object,
                _baseUrl,
                _endpointName);

            SetClientResponse();
        }

        [Ignore]
        [TestMethod]
        public async Task GetMeterialName_WithEnglish_CallsEndpointSuccessfully()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var language = Enums.Language.English;
            var materialName = "name";
            SetClientResponse(HttpStatusCode.OK, materialName);
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/site/material/{materialId}/Name?language={language}";

            // Act
            var name = await _httpSiteMaterialService.GetMeterialName(
                id,
                null,
                materialId,
                language);

            // Assert
            Assert.IsTrue(name.Equals(materialName));
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        [TestMethod]
        public async Task GetMeterialName_WithWelsh_CallsEndpointSuccessfully()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var language = Enums.Language.Welsh;
            var materialName = "name_in_welsh";
            SetClientResponse(HttpStatusCode.OK, materialName);
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/site/material/{materialId}/Name?language={language}";

            // Act
            var name = await _httpSiteMaterialService.GetMeterialName(
                id,
                null,
                materialId,
                language);

            // Assert
            Assert.IsTrue(name.Equals(materialName));
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        [Ignore]
        [TestMethod]
        public async Task GetWasteSource_ForSite_CallsEndpointSuccesfully()
        {
            // Arrange
            var siteType = SiteType.Site;
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var wasteSource = "Site_Source";
            SetClientResponse(HttpStatusCode.OK, wasteSource);
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/site/Material/{materialId}/WasteSource";

            // Act
            var result = await _httpSiteMaterialService.GetWasteSource(
                siteType,
                id,
                null,
                materialId);

            // Assert
            Assert.IsTrue(result.Equals(wasteSource));
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        [TestMethod]
        public async Task GetWasteSource_ForOverseasSite_CallsEndpointSuccesfully()
        {
            // Arrange
            var siteType = SiteType.OverseasSite;
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var wasteSource = "Site_Source";
            SetClientResponse(HttpStatusCode.OK, wasteSource);
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/overseassite/{siteId}/Material/{materialId}/WasteSource";

            // Act
            var result = await _httpSiteMaterialService.GetWasteSource(
                siteType,
                id,
                siteId,
                materialId);

            // Assert
            Assert.IsTrue(result.Equals(wasteSource));
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        [TestMethod]
        public async Task UpdateWasteSource_ForSite_CallsEndpointSuccesfully_WithExpectedPayload()
        {
            // Arrange
            var siteType = SiteType.Site;
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var wasteSource = "Site_Source";
            SetClientResponse(HttpStatusCode.OK);
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/site/Material/{materialId}/WasteSource";

            // Act
            await _httpSiteMaterialService.UpdateWasteSource(
                siteType,
                id,
                null,
                materialId,
                wasteSource);

            // Assert
            _capturedPayload = JsonConvert.DeserializeObject<string>(_capturedPayload); // comes back as json, so need to deserialize
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
            Assert.AreEqual(wasteSource, _capturedPayload);
        }

        [Ignore]
        [TestMethod]
        public async Task UpdateWasteSource_ForOverseasSite_CallsEndpointSuccesfully_WithExpectedPayload()
        {
            // Arrange
            var siteType = SiteType.OverseasSite;
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var wasteSource = "Site_Source";
            SetClientResponse(HttpStatusCode.OK);
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/overseassite/{siteId}/Material/{materialId}/WasteSource";

            // Act
            await _httpSiteMaterialService.UpdateWasteSource(
                siteType,
                id,
                siteId,
                materialId,
                wasteSource);

            // Assert
            _capturedPayload = JsonConvert.DeserializeObject<string>(_capturedPayload); // comes back as json, so need to deserialize
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
            Assert.AreEqual(wasteSource, _capturedPayload);
        }

        [TestMethod]
        public async Task GetMaterialOutputs_CallsEndPointSuccesfully_WithExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var expectedMaterialOutputsDto = new MaterialOutputsDto
            {
                TonnesContaminents = 1.2M,
                TonnesNotProcessedOnSite = 4.5M,
                TonnesProcessLoss = 56.3M
            };
            SetClientResponse(HttpStatusCode.OK, expectedMaterialOutputsDto);

            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/Site/Material/{materialId}/MaterialOutputs";

            // Act
            var materialOutputsDto = await _httpSiteMaterialService.GetMaterialOutputs(
                id,
                materialId);

            // Assert
            Assert.IsNotNull(materialOutputsDto);
            Assert.IsTrue(AreObjectsEqual(expectedMaterialOutputsDto, materialOutputsDto)); // check to ensure what is returned from the HttpClient is returned by the service
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        [TestMethod]
        public async Task GetMaterialWasteOutputs_CallsEndPointSuccesfully_WithExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var expectedMaterialWasteOutputsDto = new MaterialWasteOutputsDto
            {
                UkPackagingWaste = 1.2M,
                NonUkPackagingWaste = 4.5M,
                NonPackagingWaste = 56.3M,
            };
            this.SetClientResponse(HttpStatusCode.OK, expectedMaterialWasteOutputsDto);

            var expectedUrl = $"{this._baseUrl}/{this._endpointName}/{id}/Site/Material/{materialId}/MaterialWasteOutputs";

            // Act
            var materialWasteOutputsDto = await this._httpSiteMaterialService.GetMaterialWasteOutputs(
                id,
                materialId);

            // Assert
            Assert.IsNotNull(materialWasteOutputsDto);
            Assert.IsTrue(this.AreObjectsEqual(expectedMaterialWasteOutputsDto, materialWasteOutputsDto)); // check to ensure what is returned from the HttpClient is returned by the service
            Assert.AreEqual(expectedUrl.ToLower(), this._capturedUrl.ToLower());
        }

        [TestMethod]
        public async Task UpdateMaterialWasteOutputs_CallsEndPointSuccesfully_WithExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var materialWasteOutputsDto = new MaterialWasteOutputsDto
            {
                UkPackagingWaste = 8.65M,
                NonUkPackagingWaste = 5.09M,
                NonPackagingWaste = null,
            };

            var expectedUrl = $"{this._baseUrl}/{this._endpointName}/{id}/Site/Material/{materialId}/MaterialWasteOutputs";

            // Act
            await this._httpSiteMaterialService.UpdateMaterialWasteOutputs(
                id,
                materialId,
                materialWasteOutputsDto);

            // Arrange
            var capturedPayload = JsonConvert.DeserializeObject<MaterialWasteOutputsDto>(this._capturedPayload);
            Assert.AreEqual(expectedUrl.ToLower(), this._capturedUrl.ToLower());
            Assert.IsTrue(this.AreObjectsEqual(materialWasteOutputsDto, capturedPayload));
        }

        [TestMethod]
        public async Task UpdateMaterialOutputs_CallsEndPointSuccesfully_WithExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var materialOutputsDto = new MaterialOutputsDto
            {
                TonnesContaminents = 8.65M,
                TonnesNotProcessedOnSite = 5.09M,
                TonnesProcessLoss = null
            };

            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/Site/Material/{materialId}/MaterialOutputs";

            // Act
            await _httpSiteMaterialService.UpdateMaterialOutputs(
                id,
                materialId,
                materialOutputsDto);

            // Arrange
            var capturedPayload = JsonConvert.DeserializeObject<MaterialOutputsDto>(_capturedPayload);
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
            Assert.IsTrue(AreObjectsEqual(materialOutputsDto, capturedPayload));
        }

        [TestMethod]
        public async Task GetReprocessedWasteLastYear_CallsEndPointSuccesfully_WithExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var expectedOutput = true;
            SetClientResponse(HttpStatusCode.OK, expectedOutput);

            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/Site/Material/{materialId}/WasteLastYear";

            // Act
            var result = await _httpSiteMaterialService.GetReprocessedWasteLastYear(
                id,
                materialId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        [TestMethod]
        public async Task UpdateReprocessedWasteLastYear_CallsEndPointSuccesfully_WithExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var reprocessedWasteLastYearDto = new ReprocessedWasteLastYear
            {
                HasReprocessedWasteLastYear = false
            };

            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/Site/Material/{materialId}/WasteLastYear";

            // Act
            await _httpSiteMaterialService.UpdateReprocessedWasteLastYear(
                id,
                materialId,
                reprocessedWasteLastYearDto);

            // Arrange
            var capturedPayload = JsonConvert.DeserializeObject<ReprocessedWasteLastYear>(_capturedPayload);
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
            Assert.IsTrue(AreObjectsEqual(reprocessedWasteLastYearDto, capturedPayload));
        }

        [TestMethod]
        public async Task GetNonWasteInputs_CallsExpectedEndPoint_WithCorrectParameters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/Site/Material/{materialId}/NonWasteInputs";

            // Act
            await _httpSiteMaterialService.GetNonWasteInputs(
                id,
                materialId);

            // Assert
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        [TestMethod]
        public async Task UpdateNonWasteInputs_CallsExpectedEndPoint_WithCorrectParametersAndPayload()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var nonWasteInputsDto = new ReprocessingSupportingInformationDto();
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/Site/Material/{materialId}/NonWasteInputs";

            // Act
            await _httpSiteMaterialService.UpdateNonWasteInputs(
                id,
                materialId,
                nonWasteInputsDto);

            // Assert
            var capturedPayload = JsonConvert.DeserializeObject<ReprocessingSupportingInformationDto>(_capturedPayload);
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
            Assert.IsTrue(AreObjectsEqual(nonWasteInputsDto, capturedPayload));
        }

        [TestMethod]
        public async Task GetWasteDescriptionCodes_CallsEndPointWithParameters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/OverseasSite/{siteId}/Material/{materialId}/WasteDescriptionCodes";

            // Act
            await _httpSiteMaterialService.GetWasteDescriptionCodes(
                id,
                siteId,
                materialId);

            // Assert
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        [TestMethod]
        public async Task SaveWasteDescriptionCodes_CallEndPointWithParameters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/OverseasSite/{siteId}/Material/{materialId}/WasteDescriptionCodes";

            var wasteDecriptionCodes = new List<string>
            {
                "ABC",
                "DEF"
            };

            // Act
            await _httpSiteMaterialService.SaveWasteDescriptionCodes(
                id,
                siteId,
                materialId,
                wasteDecriptionCodes);

            // Assert
            var capturedPayload = JsonConvert.DeserializeObject<List<string>>(_capturedPayload);
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
            Assert.IsTrue(AreObjectsEqual(wasteDecriptionCodes, capturedPayload));
        }

        private bool AreObjectsEqual<T>(T obj1, T obj2)
        {
            var obj1Json = JsonConvert.SerializeObject(obj1);
            var obj2Json = JsonConvert.SerializeObject(obj2);

            return obj1Json == obj2Json;
        }

        private void SetClientResponse(
            HttpStatusCode httpStatusCode = HttpStatusCode.OK,
            object content = null)
        {
            var response = new HttpResponseMessage(httpStatusCode);

            if (content != null)
            {
                response.Content = new StringContent(JsonConvert.SerializeObject(content));
            }

            _clientHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, cancellationToken) =>
                {
                    _capturedUrl = request.RequestUri.ToString().TrimEnd('/');
                    _capturedPayload = request.Content?.ReadAsStringAsync().Result; // Read the content as string
                })
                .ReturnsAsync(response)
                .Verifiable();
        }
    }
}
