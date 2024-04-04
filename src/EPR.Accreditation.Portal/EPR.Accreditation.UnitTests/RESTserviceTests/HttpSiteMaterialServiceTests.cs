using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Portal.Common.Dtos.Portal;
using EPR.Accreditation.Portal.Enums;
using EPR.Accreditation.Portal.RESTservices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Enums = EPR.Accreditation.Portal.Enums;

namespace EPR.Accreditation.UnitTests.RESTserviceTests
{
    [TestClass]
    public class HttpSiteMaterialServiceTests
    {
        protected HttpSiteMaterialService _httpSiteMaterialService;
        protected Mock<IHttpContextAccessor> _contextAccessor;
        protected Mock<IHttpClientFactory> _httpClientFactory;
        protected HttpClient _httpClient;
        protected Mock<DelegatingHandler> _clientHandlerMock;
        protected string _baseUrl = "http://baseUrl";
        protected string _endpointName = "endpointName";
        protected string _capturedUrl;
        protected string _capturedPayload;

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

        [TestMethod]
        public async Task GetMeterialName_WithEnglish_CallsEndpointSuccessfully()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var language = Enums.Language.English;
            var materialName = "name";
            SetClientResponse(HttpStatusCode.OK, materialName);
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/site/{siteId}/material/{materialId}/Name?language={language}";

            // Act
            var name = await _httpSiteMaterialService.GetMeterialName(
                id,
                siteId,
                materialId,
                language);

            // Assert
            Assert.IsTrue(name.Equals(materialName));
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        public async Task GetMeterialName_WithWelsh_CallsEndpointSuccessfully()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var language = Enums.Language.Welsh;
            var materialName = "name_in_welsh";
            SetClientResponse(HttpStatusCode.OK, materialName);
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/site/{siteId}/material/{materialId}/Name?language={language}";

            // Act
            var name = await _httpSiteMaterialService.GetMeterialName(
                id,
                siteId,
                materialId,
                language);

            // Assert
            Assert.IsTrue(name.Equals(materialName));
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        [TestMethod]
        public async Task GetWasteSource_ForSite_CallsEndpointSuccesfully()
        {
            // Arrange
            var siteType = SiteType.Site;
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var wasteSource = "Site_Source";
            SetClientResponse(HttpStatusCode.OK, wasteSource);
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/site/{siteId}/Material/{materialId}/WasteSource";

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
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var wasteSource = "Site_Source";
            SetClientResponse(HttpStatusCode.OK);
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/site/{siteId}/Material/{materialId}/WasteSource";

            // Act
            await _httpSiteMaterialService.UpdateWasteSource(
                siteType,
                id,
                siteId,
                materialId,
                wasteSource);

            // Assert
            _capturedPayload = JsonConvert.DeserializeObject<string>( _capturedPayload ); // comes back as json, so need to deserialize
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
            Assert.AreEqual(wasteSource, _capturedPayload);
        }

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
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var expectedMaterialOutputsDto = new MaterialOutputsDto
            {
                TonnesContaminents = 1.2M,
                TonnesNotProcessedOnSite = 4.5M,
                TonnesProcessLoss = 56.3M
            };
            SetClientResponse(HttpStatusCode.OK, expectedMaterialOutputsDto);

            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/Site/{siteId}/Material/{materialId}/MaterialOutputs";

            // Act
            var materialOutputsDto = await _httpSiteMaterialService.GetMaterialOutputs(
                id,
                siteId,
                materialId);

            // Assert
            Assert.IsNotNull(materialOutputsDto);
            Assert.IsTrue(AreObjectsEqual(expectedMaterialOutputsDto, materialOutputsDto)); // check to ensure what is returned from the HttpClient is returned by the service
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        [TestMethod]
        public async Task UpdateMaterialOutputs_CallsEndPointSuccesfully_WithExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var materialOutputsDto = new MaterialOutputsDto
            {
                TonnesContaminents = 8.65M,
                TonnesNotProcessedOnSite = 5.09M,
                TonnesProcessLoss = null
            };

            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/Site/{siteId}/Material/{materialId}/MaterialOutputs";

            // Act
            await _httpSiteMaterialService.UpdateMaterialOutputs(
                id,
                siteId,
                materialId,
                materialOutputsDto);

            // Arrange
            var capturedPayload = JsonConvert.DeserializeObject<MaterialOutputsDto>(_capturedPayload);
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
            Assert.IsTrue(AreObjectsEqual(materialOutputsDto, capturedPayload));
        }

        private bool AreObjectsEqual<T>(T obj1, T obj2)
        {
            var obj1Json = JsonConvert.SerializeObject(obj1);
            var obj2Json = JsonConvert.SerializeObject(obj2);

            return obj1Json == obj2Json;
        }
    }
}
