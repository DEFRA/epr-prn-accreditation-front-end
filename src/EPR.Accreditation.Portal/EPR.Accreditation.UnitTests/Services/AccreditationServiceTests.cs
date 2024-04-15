namespace EPR.Accreditation.UnitTests.Services
{
    using AutoMapper;
    using EPR.Accreditation.Portal.Options;
    using EPR.Accreditation.Portal.RESTservices.Interfaces;
    using EPR.Accreditation.Portal.Services.Accreditation;
    using EPR.Accreditation.Portal.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Moq;

    [TestClass]
    public class AccreditationServiceTests
    {
        private Mock<IMapper> _mockMapper = null;
        private Mock<IHttpAccreditationService> _httpAccreditionService;
        private AccreditationService _accreditationService;
        private Mock<IOptions<AppSettingsConfigOptions>> _mockConfigSettings;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        [TestInitialize]
        public void Init()
        {
            var mockConfig = new AppSettingsConfigOptions
            {
                DaysUntilExpiration = 30
            };

            _mockMapper = new Mock<IMapper>();
            _mockConfigSettings = new Mock<IOptions<AppSettingsConfigOptions>>();
            _httpAccreditionService = new Mock<IHttpAccreditationService>();
            _mockConfigSettings.Setup(o => o.Value).Returns(mockConfig);
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            _accreditationService = new AccreditationService(_mockMapper.Object, _httpAccreditionService.Object, _mockHttpContextAccessor.Object);
        }

        [TestMethod]
        public void GetWasteLicensesAndPermitsViewModel_ReturnsCorrectViewModel()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = _accreditationService?.GetWastePermitViewModel(id);

            // Asset
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(WasteLicensesAndPermitsViewModel));
        }

        [TestMethod]
        public void GetWasteLicensesAndPermitsViewModel_CheckService_Call()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedDto = new Portal.DTOs.WastePermit.LicensesAndPermitsReferences();
            var expectedViewModel = new WasteLicensesAndPermitsViewModel();

            expectedDto = new Portal.DTOs.WastePermit.LicensesAndPermitsReferences()
            {
                AccreditationId = 1,
                EnvironmentalPermitNumber = "1",
                DealerRegistrationNumber = "1",
                PartAActivityReferenceNumber = "1",
                PartBActivityReferenceNumber = "1",
            };

            _httpAccreditionService.Setup(service => service.GetWastePermit(id))
                .ReturnsAsync(expectedDto);

            // Act
            var result = _accreditationService?.GetWastePermitViewModel(id);

            // Asset
            Assert.IsNotNull(result);
            _httpAccreditionService.Verify(
                s =>
                    s.GetWastePermit(
                        It.Is<Guid>(p => p == id)),
                Times.Once);

            _httpAccreditionService.Verify(
                s =>
                    s.GetWastePermit(id),
                Times.Once);
        }
    }
}
