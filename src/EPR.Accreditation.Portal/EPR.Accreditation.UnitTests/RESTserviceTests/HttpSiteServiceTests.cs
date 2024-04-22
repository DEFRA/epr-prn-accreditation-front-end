using EPR.Accreditation.Portal.DTOs.Site;
using EPR.Accreditation.Portal.DTOs.WastePermit;
using EPR.Accreditation.Portal.RESTservices;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace EPR.Accreditation.UnitTests.RESTserviceTests
{
    [TestClass]
    public class HttpSiteServiceTests
    {
        protected HttpSiteService _httpSiteService;
        protected Mock<IHttpContextAccessor> _contextAccessor;
        protected Mock<IHttpClientFactory> _httpClientFactory;
        protected HttpClient _httpClient;
        protected Mock<DelegatingHandler> _clientHandlerMock;
        protected string _baseUrl = "http://baseUrl";
        protected string _endpointName = "endpointName";
        protected string _capturedUrl;
        protected string _capturedPayload;

        [TestInitialize]
        public void Init()
        {
            _clientHandlerMock = new Mock<DelegatingHandler>();

            _clientHandlerMock.As<IDisposable>().Setup(s => s.Dispose());
            _httpClient = new HttpClient(_clientHandlerMock.Object);

            _contextAccessor = new Mock<IHttpContextAccessor>();
            _httpClientFactory = new Mock<IHttpClientFactory>(MockBehavior.Strict);

            _httpClientFactory.Setup(f => f.CreateClient(string.Empty)).Returns(_httpClient).Verifiable();
            _httpSiteService = new HttpSiteService(
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
        public async Task GetSite_CallsEndpointSuccesfully()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            SetClientResponse(HttpStatusCode.OK);
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/Site?siteexternalid={siteId}";
            SetClientResponse(HttpStatusCode.OK, new Site());

            // Act
            var result = await _httpSiteService.GetSite(id, siteId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        [TestMethod]
        public async Task GetSite_CallsEndpointSuccesfully_WithExpectedPayload()
        {
            // Arrange
            var id = Guid.NewGuid();
            var site = new Portal.DTOs.Site.Site();
            var hasExemptionReference = new PermitExemption { HasPermitExemption = false };
            SetClientResponse(HttpStatusCode.OK);
            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/Site";

            // Act
            await _httpSiteService.UpdateSite(
                id,
                site);

            // Assert
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }
    }
}
