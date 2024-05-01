using EPR.Accreditation.Portal.Common.Dtos.Portal;
using EPR.Accreditation.Portal.RESTservices;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EPR.Accreditation.UnitTests.RESTserviceTests
{
    [TestClass]
    public class HttpAccreditationServiceTests
    {
        private HttpAccreditationService _httpAccreditationService;
        private Mock<IHttpContextAccessor> _contextAccessor;
        private Mock<IHttpClientFactory> _httpClientFactory;
        private HttpClient _httpClient;
        private Mock<DelegatingHandler> _clientHandlerMock;
        private string _baseUrl = "http://baseUrl";
        private string _endpointName = "endpointName";
        private string _capturedUrl;
        private string _capturedPayload;
        private string _requestVerb;

        [TestInitialize]
        public void Init()
        {
            _clientHandlerMock = new Mock<DelegatingHandler>();

            _clientHandlerMock.As<IDisposable>().Setup(s => s.Dispose());
            _httpClient = new HttpClient(_clientHandlerMock.Object);

            _contextAccessor = new Mock<IHttpContextAccessor>();
            _httpClientFactory = new Mock<IHttpClientFactory>(MockBehavior.Strict);

            _httpClientFactory.Setup(f => f.CreateClient(string.Empty)).Returns(_httpClient).Verifiable();
            _httpAccreditationService = new HttpAccreditationService(
                _contextAccessor.Object,
                _httpClientFactory.Object,
                _baseUrl,
                _endpointName);

            SetClientResponse();
        }

        [TestMethod]
        public async Task GetLegalDocumentsAddress_CallsCorrectUrl()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/LegalDocumentAddress";

            // Act
            await _httpAccreditationService.GetLegalDocumentsAddress(id);

            // Assert
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
            Assert.AreEqual(HttpMethod.Get.Method, _requestVerb);
        }

        [TestMethod]
        public async Task UpdateLegalDocumentsAddress()
        {
            // Arrange
            var id = Guid.NewGuid();
            var address = new AddressDto
            {
                Address1 = "Address 1",
                Address2 = "Address 2",
                County = "County",
                Town = "Town",
                Postcode = "Postcode"
            };
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/LegalDocumentAddress";

            // Act
            await _httpAccreditationService.UpdateLegalDocumentsAddress(id, address);

            // Assert
            var expectedPayloadString = JsonConvert.SerializeObject(address); // comes back as json, so need to deserialize
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
            Assert.AreEqual(expectedPayloadString.ToLower(), _capturedPayload.ToLower());
            Assert.AreEqual(HttpMethod.Put.Method, _requestVerb);
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
                    _requestVerb = request.Method.Method;
                    _capturedUrl = request.RequestUri.ToString().TrimEnd('/');
                    _capturedPayload = request.Content?.ReadAsStringAsync().Result; // Read the content as string
                })
                .ReturnsAsync(response)
                .Verifiable();
        }
    }
}
