namespace EPR.Accreditation.UnitTests.RESTserviceTests
{
    using System.Net;
    using EPR.Accreditation.Portal.DTOs.OverseasSite;
    using EPR.Accreditation.Portal.RESTservices;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Moq.Protected;
    using Newtonsoft.Json;

    [TestClass]
    public class HttpOverseasSiteServiceTests
    {
        private readonly string _baseUrl = "http://baseUrl";
        private readonly string _endpointName = "endpointName";
        private HttpOverseasSiteService _httpOverseasSiteService;
        private Mock<IHttpContextAccessor> _contextAccessor;
        private Mock<IHttpClientFactory> _httpClientFactory;
        private HttpClient _httpClient;
        private Mock<DelegatingHandler> _clientHandlerMock;
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
            _httpOverseasSiteService = new HttpOverseasSiteService(
                _contextAccessor.Object,
                _httpClientFactory.Object,
                _baseUrl,
                _endpointName);

            SetClientResponse();
        }

        [TestMethod]
        public async Task GetReprocessorDetails_CallsEndpointSuccesfully()
        {
            // Arrange
            var id = Guid.NewGuid();
            var overseasSiteId = Guid.NewGuid();
            SetClientResponse(HttpStatusCode.OK);
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/OverseasSite/{overseasSiteId}/ReprocessorDetails";

            var reprocessorDetails = new ReprocessorDetailsDto
            {
                Name = "name",
                CountryId = 826,
                Address = "123 High street"
            };

            SetClientResponse(HttpStatusCode.OK, reprocessorDetails);

            // Act
            var result = await _httpOverseasSiteService.GetReprocessorDetails(id, overseasSiteId);

            // Assert
            Assert.AreEqual(reprocessorDetails.Name, result.Name);
            Assert.AreEqual(reprocessorDetails.CountryId, result.CountryId);
            Assert.AreEqual(reprocessorDetails.Address, result.Address);
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        [TestMethod]
        public async Task UpdateReprocessorDetails_CallsEndpointSuccesfully_WithExpectedPayload()
        {
            // Arrange
            var id = Guid.NewGuid();
            var overseasSiteId = Guid.NewGuid();
            SetClientResponse(HttpStatusCode.OK);
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/OverseasSite/{overseasSiteId}/ReprocessorDetails";

            var reprocessorDetails = new ReprocessorDetailsDto
            {
                Name = "name",
                CountryId = 826,
                Address = "123 High street"
            };

            SetClientResponse(HttpStatusCode.OK);

            // Act
            await _httpOverseasSiteService.UpdateReprocessorDetails(id, overseasSiteId, reprocessorDetails);

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
