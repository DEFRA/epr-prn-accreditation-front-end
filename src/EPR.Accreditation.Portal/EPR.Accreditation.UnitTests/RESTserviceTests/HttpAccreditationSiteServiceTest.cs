namespace EPR.Accreditation.UnitTests.RESTserviceTests
{
    using System.Net;
    using EPR.Accreditation.Portal.RESTservices;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Moq.Protected;
    using Newtonsoft.Json;

    [TestClass]
    public class HttpAccreditationSiteServiceTest
    {
        private HttpAccreditationSiteService _httpAccreditationSiteService;
        private Mock<IHttpContextAccessor> _contextAccessor;
        private Mock<IHttpClientFactory> _httpClientFactory;
        private HttpClient _httpClient;
        private Mock<DelegatingHandler> _clientHandlerMock;
        private string _baseUrl = "http://baseUrl";
        private string _endpointName = "endpointName";
        private string _capturedUrl;
        private string _capturedPayload;

        [TestInitialize]
        public void Init()
        {
            _clientHandlerMock = new Mock<DelegatingHandler>();

            _clientHandlerMock.As<IDisposable>().Setup(s => s.Dispose());
            _httpClient = new HttpClient(_clientHandlerMock.Object);

            _contextAccessor = new Mock<IHttpContextAccessor>();
            _httpClientFactory = new Mock<IHttpClientFactory>(MockBehavior.Strict);

            _httpClientFactory.Setup(f => f.CreateClient(string.Empty)).Returns(_httpClient).Verifiable();
            _httpAccreditationSiteService = new HttpAccreditationSiteService(
                _contextAccessor.Object,
                _httpClientFactory.Object,
                _baseUrl,
                _endpointName);

            SetClientResponse();
        }

        [TestMethod]
        public async Task GetExemptionReferences_CallsEndpointSuccesfully()
        {
            // Arrange
            var id = Guid.NewGuid();
            SetClientResponse(HttpStatusCode.OK);
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/Site/ExemptionReferences";
            var exemptionReferences = new List<string>
            {
                "REF001",
                "REF002",
                "REF003"
            };

            SetClientResponse(HttpStatusCode.OK, exemptionReferences);

            // Act
            var result = await _httpAccreditationSiteService.GetExemptionReferences(id);

            // Assert
            Assert.IsTrue(exemptionReferences.SequenceEqual(result));
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        [TestMethod]
        public async Task GetExemptionReferences_CallsEndpointSuccesfully_WithExpectedPayload()
        {
            // Arrange
            var id = Guid.NewGuid();
            var exemptionReferences = new List<string>
            {
                "REF001",
                "REF002",
                "REF003"
            };

            SetClientResponse(HttpStatusCode.OK);
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/Site/ExemptionReferences";

            // Act
            await _httpAccreditationSiteService.UpdateExemptionReferences(id, exemptionReferences);

            // Assert
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
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
